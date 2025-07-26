using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Travellin.Core.Dtos.Conversation;
using Travellin.Core.Dtos.Message;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;

namespace Travellin.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IConversationService _conversationService;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(
            IMessageService messageService, 
            IConversationService conversationService,
            ILogger<ChatHub> logger)
        {
            _messageService = messageService;
            _conversationService = conversationService;
            _logger = logger;
        }

        private string GetCurrentUserId()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }
            return userId;
        }

        public async Task SendMessage(CreateMessageDto dto)
        {
            try
            {
                var senderId = GetCurrentUserId();

                // Validate sender
                if (dto.SenderId != senderId)
                {
                    await Clients.Caller.SendAsync("ReceiveError", "Unauthorized sender.");
                    _logger.LogWarning("Unauthorized message attempt from {SenderId} claiming to be {ActualSender}", dto.SenderId, senderId);
                    return;
                }

                // Validate content
                if (string.IsNullOrWhiteSpace(dto.Content))
                {
                    await Clients.Caller.SendAsync("ReceiveError", "Message content cannot be empty.");
                    return;
                }

                // Create and persist message using service
                var message = new Message
                {
                    SenderId = dto.SenderId,
                    ReceiverId = dto.ReceiverId,
                    Content = dto.Content.Trim(),
                    IsRead = false,
                    SentAt = DateTime.UtcNow
                };

                var savedMessage = await _messageService.SendMessageAsync(message);

                var messageDto = new MessageDto
                {
                    Id = savedMessage.Id,
                    SenderId = savedMessage.SenderId,
                    ReceiverId = savedMessage.ReceiverId,
                    Content = savedMessage.Content,
                    SentAt = savedMessage.SentAt,
                    IsRead = savedMessage.IsRead,
                    TranslatedContent = savedMessage.TranslatedContent,
                    ConversationId = savedMessage.ConversationId
                };

                // Send to receiver if online
                await Clients.User(dto.ReceiverId).SendAsync("ReceiveMessage", messageDto);

                // Send confirmation to sender
                await Clients.Caller.SendAsync("MessageSent", messageDto);

                _logger.LogInformation("Message sent from {SenderId} to {ReceiverId}", dto.SenderId, dto.ReceiverId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message from {SenderId} to {ReceiverId}", dto.SenderId, dto.ReceiverId);
                await Clients.Caller.SendAsync("ReceiveError", "Failed to send message. Please try again.");
            }
        }

        public async Task StartConversation(StartConversationDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                if (userId != dto.User1Id)
                {
                    await Clients.Caller.SendAsync("ReceiveError", "Unauthorized to start conversation.");
                    _logger.LogWarning("Unauthorized conversation start attempt by {UserId} for conversation between {User1} and {User2}", 
                        userId, dto.User1Id, dto.User2Id);
                    return;
                }

                var conversation = await _conversationService.CreateOrGetConversationAsync(dto.User1Id, dto.User2Id);

                var conversationDto = new ConversationDto
                {
                    Id = conversation.Id,
                    User1Id = conversation.User1Id,
                    User2Id = conversation.User2Id,
                    Messages = new List<MessageDto>()
                };

                // Notify both users
                await Clients.User(dto.User2Id).SendAsync("NewConversationStarted", conversationDto);
                await Clients.User(dto.User1Id).SendAsync("NewConversationStarted", conversationDto);

                _logger.LogInformation("Conversation started between {User1} and {User2}", dto.User1Id, dto.User2Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting conversation between {User1} and {User2}", dto.User1Id, dto.User2Id);
                await Clients.Caller.SendAsync("ReceiveError", "Failed to start conversation. Please try again.");
            }
        }

        public async Task MarkMessageAsRead(int messageId)
        {
            try
            {
                var userId = GetCurrentUserId();
                
                if (!await _messageService.CanUserMarkMessageAsRead(messageId, userId))
                {
                    await Clients.Caller.SendAsync("ReceiveError", "Unauthorized to mark message as read.");
                    return;
                }

                await _messageService.MarkMessageAsReadAsync(messageId);
                await Clients.Caller.SendAsync("MessageMarkedAsRead", messageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking message {MessageId} as read by {UserId}", messageId, GetCurrentUserId());
                await Clients.Caller.SendAsync("ReceiveError", "Failed to mark message as read.");
            }
        }

        public async Task MarkConversationAsRead(int conversationId)
        {
            try
            {
                var userId = GetCurrentUserId();
                
                if (!await _messageService.UserIsInConversationAsync(conversationId, userId))
                {
                    await Clients.Caller.SendAsync("ReceiveError", "Unauthorized to mark conversation as read.");
                    return;
                }

                await _messageService.MarkMessagesAsReadAsync(conversationId, userId);
                await Clients.Caller.SendAsync("ConversationMarkedAsRead", conversationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking conversation {ConversationId} as read by {UserId}", conversationId, GetCurrentUserId());
                await Clients.Caller.SendAsync("ReceiveError", "Failed to mark conversation as read.");
            }
        }

        public async Task JoinConversation(int conversationId)
        {
            try
            {
                var userId = GetCurrentUserId();
                
                if (!await _messageService.UserIsInConversationAsync(conversationId, userId))
                {
                    await Clients.Caller.SendAsync("ReceiveError", "Unauthorized to join conversation.");
                    return;
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, $"conversation_{conversationId}");
                await Clients.Caller.SendAsync("JoinedConversation", conversationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error joining conversation {ConversationId} by {UserId}", conversationId, GetCurrentUserId());
                await Clients.Caller.SendAsync("ReceiveError", "Failed to join conversation.");
            }
        }

        public async Task LeaveConversation(int conversationId)
        {
            try
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"conversation_{conversationId}");
                await Clients.Caller.SendAsync("LeftConversation", conversationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error leaving conversation {ConversationId} by {UserId}", conversationId, GetCurrentUserId());
            }
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var userId = GetCurrentUserId();
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
                
                _logger.LogInformation("User {UserId} connected to chat hub", userId);
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnConnectedAsync");
                throw;
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var userId = GetCurrentUserId();
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
                
                _logger.LogInformation("User {UserId} disconnected from chat hub", userId);
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnDisconnectedAsync");
                await base.OnDisconnectedAsync(exception);
            }
        }
    }
}