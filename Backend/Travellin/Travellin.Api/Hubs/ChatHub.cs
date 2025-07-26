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

        public ChatHub(IMessageService messageService, IConversationService conversationService)
        {
            _messageService = messageService;
            _conversationService = conversationService;
        }

        private string GetCurrentUserId() =>
            Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        public async Task SendMessage(CreateMessageDto dto)
        {
            var senderId = GetCurrentUserId();

            // Validate sender
            if (dto.SenderId != senderId)
            {
                await Clients.Caller.SendAsync("ReceiveError", "Unauthorized sender.");
                return;
            }

            // Create and persist message using service (ensures all checks/logic)
            var message = new Message
            {
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                Content = dto.Content,
                IsRead = false
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

            // Optionally send to sender as confirmation
            await Clients.Caller.SendAsync("MessageSent", messageDto);
        }

        public async Task StartConversation(StartConversationDto dto)
        {
            var userId = GetCurrentUserId();

            if (userId != dto.User1Id)
            {
                await Clients.Caller.SendAsync("ReceiveError", "Unauthorized to start conversation.");
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

            // Notify both users (starter and receiver)
            await Clients.User(dto.User2Id).SendAsync("NewConversationStarted", conversationDto);
            await Clients.User(dto.User1Id).SendAsync("NewConversationStarted", conversationDto);
        }


        public override async Task OnConnectedAsync()
        {
            var userId = GetCurrentUserId();
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetCurrentUserId();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}