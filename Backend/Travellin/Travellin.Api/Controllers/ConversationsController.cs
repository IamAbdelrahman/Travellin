using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travellin.Core.Dtos.Conversation;
using Travellin.Core.Dtos.Message;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;

namespace Travellin.Travellin.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Tags("Conversation")]

public class ConversationsController : ControllerBase
{
    private readonly IConversationService _conversationService;
    private readonly IUnitOfWork _unitOfWork;
    
    private string GetCurrentUserId() =>
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;


    public ConversationsController(IConversationService conversationService, IUnitOfWork unitOfWork)
    {
        _conversationService = conversationService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("start")]
    [EndpointSummary("Starts a new conversation or returns the existing one between two users.")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ConversationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StartConversation([FromBody] StartConversationDto dto)
    {
        Console.WriteLine("=== StartConversation method called ===");
        Console.WriteLine($"Received DTO: User1Id={dto?.User1Id}, User2Id={dto?.User2Id}, PropertyId={dto?.PropertyId}");
        
        try
        {
            var currentUserId = GetCurrentUserId();
            Console.WriteLine($"Current user ID: {currentUserId}");
            
            if (currentUserId == null)
            {
                Console.WriteLine("Current user ID is null - unauthorized");
                return Unauthorized();
            }

            // Validate that the current user is one of the participants
            if (currentUserId != dto.User1Id && currentUserId != dto.User2Id)
            {
                Console.WriteLine($"Current user {currentUserId} is not a participant in the conversation");
                return BadRequest("You can only start conversations where you are a participant");
            }

            Console.WriteLine($"Starting conversation between {dto.User1Id} and {dto.User2Id} with property {dto.PropertyId}");

            var conversation = await _conversationService.CreateOrGetConversationWithPropertyAsync(
                dto.User1Id, dto.User2Id, dto.PropertyId);

            Console.WriteLine($"Conversation created/retrieved with ID: {conversation.Id}");

            // Create a proper DTO without circular references
            var result = new ConversationDto
            {
                Id = conversation.Id,
                User1Id = conversation.User1Id,
                User2Id = conversation.User2Id,
                User1Name = conversation.User1?.UserName ?? $"User {conversation.User1Id.Substring(0, 8)}",
                User2Name = conversation.User2?.UserName ?? $"User {conversation.User2Id.Substring(0, 8)}",
                PropertyId = conversation.PropertyId,
                PropertyTitle = conversation.Property?.Title,
                CreatedAt = conversation.CreatedAt,
                Messages = conversation.Messages?.Select(m => new MessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    ConversationId = m.ConversationId,
                    IsRead = m.IsRead,
                    SentAt = m.SentAt,
                    TranslatedContent = m.TranslatedContent
                }).ToList() ?? new List<MessageDto>()
            };
            
            Console.WriteLine($"Created DTO with {result.Messages.Count} messages");
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in StartConversation: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return StatusCode(500, new { error = ex.Message, details = ex.StackTrace });
        }
    }

    [HttpGet("by-user/{userId}")]
    [EndpointSummary("Retrieves all conversations for the given user.")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ConversationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserConversations(string userId)
    {
        var currentUserId = GetCurrentUserId();

        // Ensure users can only access their own conversations (unless they're admin)
        if (userId != currentUserId)
        {
            return Forbid();
        }

        var conversations = await _conversationService.GetUserConversationsAsync(userId);

        var result = conversations.Select(c => new ConversationDto
        {
            Id = c.Id,
            User1Id = c.User1Id,
            User2Id = c.User2Id,
            User1Name = c.User1?.UserName ?? $"User {c.User1Id.Substring(0, 8)}",
            User2Name = c.User2?.UserName ?? $"User {c.User2Id.Substring(0, 8)}",
            PropertyId = c.PropertyId,
            PropertyTitle = c.Property?.Title,
            CreatedAt = c.CreatedAt,
            Messages = c.Messages.Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                ConversationId = m.ConversationId,
                IsRead = m.IsRead,
                SentAt = m.SentAt,
                TranslatedContent = m.TranslatedContent
            }).ToList()
        });

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [EndpointSummary("Retrieves a specific conversation by its ID.")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ConversationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetConversationById(int id)
    {
        var currentUserId = GetCurrentUserId();
        if (!await _conversationService.UserIsInConversationAsync(id, currentUserId))
            return Forbid();

        var conversation = await _conversationService.GetConversationByIdAsync(id);
        if (conversation == null) return NotFound();

        var result = new ConversationDto
        {
            Id = conversation.Id,
            User1Id = conversation.User1Id,
            User2Id = conversation.User2Id,
            User1Name = conversation.User1?.UserName ?? $"User {conversation.User1Id.Substring(0, 8)}",
            User2Name = conversation.User2?.UserName ?? $"User {conversation.User2Id.Substring(0, 8)}",
            PropertyId = conversation.PropertyId,
            PropertyTitle = conversation.Property?.Title,
            CreatedAt = conversation.CreatedAt,
            Messages = conversation.Messages.Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                ConversationId = m.ConversationId,
                IsRead = m.IsRead,
                SentAt = m.SentAt,
                TranslatedContent = m.TranslatedContent
            }).ToList()
        };

        return Ok(result);
    }

    [HttpDelete("admin/{id:int}")]
    [Authorize(Roles = "Admin")]
    [EndpointSummary("Deletes a conversation by its ID (Admin only).")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteConversationAsAdmin(int id)
    {
        var deleted = await _conversationService.DeleteConversationAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Deletes a conversation by its ID.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteConversation(int id)
    {
        var deleted = await _conversationService.DeleteConversationAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }

    [HttpGet("inbox/{userId}")]
    [ProducesResponseType(typeof(List<InboxDto>), StatusCodes.Status200OK)]
    [EndpointSummary("Get inbox preview for user")]
    public async Task<IActionResult> GetInboxPreview(string userId)
    {
        var currentUserId = GetCurrentUserId();
        
        // Ensure users can only access their own inbox (unless they're admin)
        if (userId != currentUserId)
        {
            return Forbid();
        }
        
        var result = await _conversationService.GetInboxPreviewAsync(userId);
        return Ok(result);
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(List<ConversationSearchResultDto>), StatusCodes.Status200OK)]
    [EndpointSummary("Search conversations by participant name or message content")]
    public async Task<IActionResult> Search([FromQuery] string userId, [FromQuery] string query)
    {

        var currentUserId = GetCurrentUserId();
        var results = await _conversationService.SearchConversationsAsync(currentUserId, query);
        return Ok(results);
    }

    [HttpGet("admin/all")]
    [Authorize(Roles = "Admin")]
    [EndpointSummary("Get all conversations (Admin only)")]
    [ProducesResponseType(typeof(IEnumerable<ConversationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllConversations()
    {
        var conversations = await _conversationService.GetAllConversationsAsync();

        var result = conversations.Select(c => new ConversationDto
        {
            Id = c.Id,
            User1Id = c.User1Id,
            User2Id = c.User2Id,
            User1Name = c.User1?.UserName ?? $"User {c.User1Id.Substring(0, 8)}",
            User2Name = c.User2?.UserName ?? $"User {c.User2Id.Substring(0, 8)}",
            PropertyId = c.PropertyId,
            PropertyTitle = c.Property?.Title,
            CreatedAt = c.CreatedAt,
            Messages = c.Messages.Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                ConversationId = m.ConversationId,
                IsRead = m.IsRead,
                SentAt = m.SentAt,
                TranslatedContent = m.TranslatedContent
            }).ToList()
        });

        return Ok(result);
    }

    [HttpPost("admin/send-message")]
    [Authorize(Roles = "Admin")]
    [EndpointSummary("Send message as admin to any conversation")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SendMessageAsAdmin([FromBody] CreateMessageDto dto)
    {
        var conversation = await _conversationService.GetConversationByIdAsync(dto.ConversationId);
        if (conversation == null)
            return NotFound();

        var currentUserId = GetCurrentUserId();

        // Create message as admin using the actual admin user ID
        var message = new Message
        {
            Content = dto.Content,
            SenderId = currentUserId, // Use actual admin user ID
            ReceiverId = dto.ReceiverId, // Use the receiver ID from the DTO
            ConversationId = dto.ConversationId,
            IsRead = false,
            SentAt = DateTime.UtcNow
        };

        // Save message using AddAsync
        await _unitOfWork.MessageRepository.AddAsync(message);
        await _unitOfWork.SaveChangesAsync();

        var messageDto = new MessageDto
        {
            Id = message.Id,
            Content = message.Content,
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId,
            ConversationId = message.ConversationId,
            IsRead = message.IsRead,
            SentAt = message.SentAt,
            TranslatedContent = message.TranslatedContent
        };

        return Ok(messageDto);
    }
}
