using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    public class ConversationRepository : GenericRepository<Conversation, int>, IConversationRepository
    {
        public ConversationRepository(TravellinDbContext dbContext) : base(dbContext) { }

        public async Task<Conversation?> GetBetweenUsersAsync(string user1Id, string user2Id)
        {
            return await _dbContext.Conversations
                .FirstOrDefaultAsync(c =>
                    (c.User1Id == user1Id && c.User2Id == user2Id) ||
                    (c.User1Id == user2Id && c.User2Id == user1Id));
        }

        public async Task<List<Conversation>> GetUserConversationsAsync(string userId)
        {
            return await _dbContext.Conversations
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .Include(c => c.User1)
                .Include(c => c.User2)
                .Include(c => c.Messages)
                .Include(c => c.Property)
                .ToListAsync();
        }
        public async Task<Conversation?> GetByIdWithMessagesAsync(int id)
        {
            return await _dbContext.Conversations
                .Include(c => c.User1)
                .Include(c => c.User2)
                .Include(c => c.Messages)
                .Include(c => c.Property)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<Conversation>> GetInboxPreviewAsync(string userId)
        {
            return await _dbContext.Conversations
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .Include(c => c.User1)
                    .ThenInclude(u => u.UserProfile)
                .Include(c => c.User2)
                    .ThenInclude(u => u.UserProfile)
                .Include(c => c.Property)
                .Include(c => c.Messages.OrderByDescending(m => m.SentAt).Take(1))
                .ToListAsync();
        }
        public async Task<List<Conversation>> GetUserConversationsWithMessagesAndUsersAsync(string userId)
        {
            return await _dbContext.Conversations
                .Include(c => c.User1)
                .Include(c => c.User2)
                .Include(c => c.Messages)
                .Include(c => c.Property)
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .ToListAsync();
        }

        public async Task<List<Conversation>> GetAllConversationsAsync()
        {
            return await _dbContext.Conversations
                .Include(c => c.User1)
                .Include(c => c.User2)
                .Include(c => c.Messages)
                .Include(c => c.Property)
                .ToListAsync();
        }
    }

}
