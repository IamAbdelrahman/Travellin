using Microsoft.Extensions.Logging;
using Travellin.Core.Dtos.Message;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;

namespace Travellin.Infrastructure.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepo;
    private readonly IConversationRepository _conversationRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MessageService> _logger;

    public MessageService(
        IMessageRepository messageRepo,
        IConversationRepository conversationRepo,
        IUnitOfWork unitOfWork,
        ILogger<MessageService> logger)
    {
        _messageRepo = messageRepo;
        _conversationRepo = conversationRepo;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<MessageDto> SendMessageAsync(Message message)
    {
        try
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(message.Content))
            {
                throw new ArgumentException("Message content cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(message.SenderId) || string.IsNullOrWhiteSpace(message.ReceiverId))
            {
                throw new ArgumentException("Sender and receiver IDs are required");
            }

            if (message.SenderId == message.ReceiverId)
            {
                throw new ArgumentException("Cannot send message to yourself");
            }

            // Step 1: Find or create a conversation between users
            var existingConversation = await _conversationRepo.GetBetweenUsersAsync(message.SenderId, message.ReceiverId);

            if (existingConversation == null)
            {
                var newConversation = new Conversation
                {
                    User1Id = message.SenderId,
                    User2Id = message.ReceiverId
                };

                await _conversationRepo.AddAsync(newConversation);
                await _unitOfWork.SaveChangesAsync();

                existingConversation = newConversation;
                _logger.LogInformation("Created new conversation between {User1} and {User2}", message.SenderId, message.ReceiverId);
            }

            // Step 2: Populate message fields
            message.ConversationId = existingConversation.Id;
            message.SentAt = DateTime.UtcNow;
            message.IsRead = false;

            await _messageRepo.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();

            var fullMessage = await _messageRepo.GetMessageWithDetailsAsync(message.Id);
            if (fullMessage == null)
                throw new Exception("Message could not be retrieved after sending.");

            // Step 3: Return DTO
            var messageDto = new MessageDto
            {
                Id = fullMessage.Id,
                Content = fullMessage.Content,
                IsRead = fullMessage.IsRead,
                SentAt = fullMessage.SentAt,
                TranslatedContent = fullMessage.TranslatedContent,
                ConversationId = fullMessage.ConversationId,
                SenderId = fullMessage.SenderId,
                ReceiverId = fullMessage.ReceiverId
            };

            _logger.LogInformation("Message sent from {SenderId} to {ReceiverId} in conversation {ConversationId}", 
                message.SenderId, message.ReceiverId, existingConversation.Id);

            return messageDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending message from {SenderId} to {ReceiverId}", message.SenderId, message.ReceiverId);
            throw;
        }
    }

    public async Task<List<MessageDto>> GetMessagesByConversationIdAsync(int conversationId)
    {
        try
        {
            var messages = await _messageRepo.GetMessagesByConversationIdAsync(conversationId);

            return messages.Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                IsRead = m.IsRead,
                SentAt = m.SentAt,
                TranslatedContent = m.TranslatedContent,
                ConversationId = m.ConversationId
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting messages for conversation {ConversationId}", conversationId);
            throw;
        }
    }

    public async Task MarkMessageAsReadAsync(int messageId)
    {
        try
        {
            await _messageRepo.MarkAsReadAsync(messageId);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Message {MessageId} marked as read", messageId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking message {MessageId} as read", messageId);
            throw;
        }
    }

    public async Task<int> GetUnreadCountAsync(string userId)
    {
        try
        {
            return await _messageRepo.CountUnreadMessagesAsync(userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting unread count for user {UserId}", userId);
            throw;
        }
    }

    public async Task MarkMessagesAsReadAsync(int conversationId, string userId)
    {
        try
        {
            var messages = await _messageRepo.GetUnreadMessagesByConversationAndUserAsync(conversationId, userId);
            foreach (var message in messages)
            {
                message.IsRead = true;
            }
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Marked {Count} messages as read in conversation {ConversationId} for user {UserId}", 
                messages.Count, conversationId, userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking messages as read in conversation {ConversationId} for user {UserId}", 
                conversationId, userId);
            throw;
        }
    }

    public async Task<bool> UserIsInConversationAsync(int conversationId, string currentUserId)
    {
        try
        {
            var conversation = await _conversationRepo.GetByIdAsync(conversationId);

            if (conversation == null)
                return false;

            return conversation.User1Id == currentUserId || conversation.User2Id == currentUserId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if user {UserId} is in conversation {ConversationId}", 
                currentUserId, conversationId);
            throw;
        }
    }

    public async Task<bool> CanUserMarkMessageAsRead(int messageId, string currentUserId)
    {
        try
        {
            var message = await _messageRepo.GetByIdAsync(messageId);

            if (message == null)
                return false;

            return message.ReceiverId == currentUserId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if user {UserId} can mark message {MessageId} as read", 
                currentUserId, messageId);
            throw;
        }
    }
}
