using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Entities;

namespace Travellin.Core.Interfaces
{
    public interface IConversationService
    {
        Task<Conversation> CreateOrGetConversationAsync(string user1Id, string user2Id);
        Task<List<Conversation>> GetUserConversationsAsync(string userId);
        Task<Conversation?> GetConversationByIdAsync(int conversationId);   
        Task<bool> DeleteConversationAsync(int conversationId);            
    }

}
