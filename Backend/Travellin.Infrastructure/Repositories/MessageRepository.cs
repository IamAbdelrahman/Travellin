using Microsoft.EntityFrameworkCore;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;
using Travellin.Infrastructure.Repositories;

public class MessageRepository : GenericRepository<Message, int>, IMessageRepository
{
    public MessageRepository(TravellinDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Message>> GetMessagesByConversationIdAsync(int conversationId)
    {
        return await _dbContext.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Include(m => m.Conversation)
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task MarkAsReadAsync(int messageId)
    {
        var message = await _dbContext.Messages.FindAsync(messageId);
        if (message != null)
        {
            message.IsRead = true;
        }
    }

    public async Task<Message?> GetMessageWithDetailsAsync(int messageId)
    {
        return await _dbContext.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Include(m => m.Conversation)
            .FirstOrDefaultAsync(m => m.Id == messageId);
    }
    public async Task AddAsync(Message message)
    {
        await _dbContext.Messages.AddAsync(message);
    }

}
