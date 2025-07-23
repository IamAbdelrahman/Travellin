using Travellin.Core.Dtos.Message;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;

namespace Travellin.Infrastructure.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepo;
    private readonly IConversationRepository _conversationRepo;
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(
        IMessageRepository messageRepo,
        IConversationRepository conversationRepo,
        IUnitOfWork unitOfWork)
    {
        _messageRepo = messageRepo;
        _conversationRepo = conversationRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageDto> SendMessageAsync(Message message)
    {
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
        return new MessageDto
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
    }

    public async Task<List<MessageDto>> GetMessagesByConversationIdAsync(int conversationId)
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


    public async Task MarkMessageAsReadAsync(int messageId)
    {
        await _messageRepo.MarkAsReadAsync(messageId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> GetUnreadCountAsync(string userId)
    {
        return await _messageRepo.CountUnreadMessagesAsync(userId);
    }

    public async Task MarkMessagesAsReadAsync(int conversationId, string userId)
    {
        var messages = await _messageRepo.GetUnreadMessagesByConversationAndUserAsync(conversationId, userId);
        foreach (var message in messages)
        {
            message.IsRead = true;
        }
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UserIsInConversationAsync(int conversationId, string currentUserId)
    {
        var conversation = await _conversationRepo.GetByIdAsync(conversationId);

        if (conversation == null)
            return false;

        return conversation.User1Id == currentUserId || conversation.User2Id == currentUserId;
    }

    public async Task<bool> CanUserMarkMessageAsRead(int messageId, string currentUserId)
    {
        var message = await _messageRepo.GetByIdAsync(messageId);

        if (message == null)
            return false;

        return message.ReceiverId == currentUserId;
    }

}
