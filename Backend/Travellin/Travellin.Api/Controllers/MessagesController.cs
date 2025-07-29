using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travellin.Core.Dtos.Message;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;

namespace Travellin.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [Tags("Messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IServiceFactory _serviceFactory;
        
        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public MessagesController(IMessageService messageService, IServiceFactory serviceFactory)
        {
            _messageService = messageService;
            _serviceFactory = serviceFactory;
        }

        /// <summary>
        /// Send a new message between users.
        /// </summary>
        /// <param name="dto">The message data</param>
        [HttpPost("send")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Send a new message between users")]
        public async Task<IActionResult> SendMessage([FromBody] CreateMessageDto dto)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                if (currentUserId == null)
                    return Unauthorized();

                if (dto.SenderId != currentUserId)
                    return Forbid();

                var message = new Message
                {
                    ConversationId = dto.ConversationId,
                    SenderId = dto.SenderId,
                    ReceiverId = dto.ReceiverId,
                    Content = dto.Content,
                    SentAt = DateTime.UtcNow,
                    IsRead = false
                };

                var savedMessage = await _messageService.SendMessageAsync(message);
                return Ok(savedMessage);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieve all messages in a given conversation by its ID.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation</param>
        [AllowAnonymous]
        [HttpGet("conversation/{conversationId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<MessageDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Get all messages in a conversation")]
        public async Task<IActionResult> GetMessagesByConversationId(int conversationId)
        {
            var currentUserId = GetCurrentUserId();

            if (!await _messageService.UserIsInConversationAsync(conversationId, currentUserId))
                return Forbid();

            var messages = await _messageService.GetMessagesByConversationIdAsync(conversationId);
            return Ok(messages);
        }

        /// <summary>
        /// Mark a specific message as read.
        /// </summary>
        /// <param name="id">The ID of the message</param>
        [HttpPost("{id}/mark-as-read")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Mark a message as read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var currentUserId = GetCurrentUserId();

            if (!await _messageService.CanUserMarkMessageAsRead(id, currentUserId))
                return Forbid();

            await _messageService.MarkMessageAsReadAsync(id);
            return NoContent();
        }

        [HttpGet("unread/count")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [EndpointSummary("Get total unread message count for a user")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var currentUserId = GetCurrentUserId();
            var count = await _messageService.GetUnreadCountAsync(currentUserId);
            return Ok(new { unreadCount = count });
        }

        [HttpPut("mark-read/{conversationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EndpointSummary("Mark all messages in a conversation as read")]
        public async Task<IActionResult> MarkAllMessagesAsRead(int conversationId)
        {
            var currentUserId = GetCurrentUserId();

            if (!await _messageService.UserIsInConversationAsync(conversationId, currentUserId))
                return Forbid();

            await _messageService.MarkMessagesAsReadAsync(conversationId, currentUserId);
            return NoContent();
        }
    }
}
