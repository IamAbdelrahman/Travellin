using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Entities;

namespace Travellin.Core.Interfaces
{
    public interface IConversationRepository : IGenericRepository<Conversation, int>
    {
        Task<Conversation?> GetBetweenUsersAsync(string user1Id, string user2Id);
        Task<List<Conversation>> GetUserConversationsAsync(string userId);
        Task<Conversation?> GetByIdWithMessagesAsync(int id);
        Task<List<Conversation>> GetInboxPreviewAsync(string userId);
        Task<List<Conversation>> GetUserConversationsWithMessagesAndUsersAsync(string userId);
        Task<List<Conversation>> GetAllConversationsAsync(); // New method for admin
    }

}
