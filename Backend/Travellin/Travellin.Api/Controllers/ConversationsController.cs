using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos.Conversation;
using Travellin.Core.Dtos.Message;
using Travellin.Core.Interfaces;

namespace Travellin.Travellin.Api.Controllers;

[ApiController]
[Route("api/conversations")]
public class ConversationsController : ControllerBase
{
    private readonly IConversationService _conversationService;

    public ConversationsController(IConversationService conversationService)
    {
        _conversationService = conversationService;
    }

    [HttpPost("start")]
    [EndpointSummary("Starts a new conversation or returns the existing one between two users.")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ConversationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StartConversation([FromBody] StartConversationDto dto)
    {
        var conversation = await _conversationService.CreateOrGetConversationAsync(dto.User1Id, dto.User2Id);

        var result = new ConversationDto
        {
            Id = conversation.Id,
            User1Id = conversation.User1Id,
            User2Id = conversation.User2Id,
            Messages = conversation.Messages.Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                IsRead = m.IsRead,
                SentAt = m.SentAt,
                TranslatedContent = m.TranslatedContent
            }).ToList()
        };

        return Ok(result);
    }

    [HttpGet("by-user/{userId}")]
    [EndpointSummary("Retrieves all conversations for the given user.")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ConversationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserConversations(string userId)
    {
        var conversations = await _conversationService.GetUserConversationsAsync(userId);

        if (!conversations.Any())
            return NotFound();

        var result = conversations.Select(c => new ConversationDto
        {
            Id = c.Id,
            User1Id = c.User1Id,
            User2Id = c.User2Id,
            Messages = c.Messages.Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
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
        var conversation = await _conversationService.GetConversationByIdAsync(id);
        if (conversation == null) return NotFound();

        var result = new ConversationDto
        {
            Id = conversation.Id,
            User1Id = conversation.User1Id,
            User2Id = conversation.User2Id,
            Messages = conversation.Messages.Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                IsRead = m.IsRead,
                SentAt = m.SentAt,
                TranslatedContent = m.TranslatedContent
            }).ToList()
        };

        return Ok(result);
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
        var result = await _conversationService.GetInboxPreviewAsync(userId);
        return Ok(result);
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(List<ConversationSearchResultDto>), StatusCodes.Status200OK)]
    [EndpointSummary("Search conversations by participant name or message content")]
    public async Task<IActionResult> Search([FromQuery] string userId, [FromQuery] string query)
    {
        var results = await _conversationService.SearchConversationsAsync(userId, query);
        return Ok(results);
    }
}
