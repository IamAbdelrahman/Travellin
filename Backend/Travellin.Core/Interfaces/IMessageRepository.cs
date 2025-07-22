using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Entities;

namespace Travellin.Core.Interfaces
{
    public interface IMessageRepository : IGenericRepository<Message, int>
    {
        Task<List<Message>> GetMessagesByConversationIdAsync(int conversationId);
        Task MarkAsReadAsync(int messageId);
        Task<Message?> GetMessageWithDetailsAsync(int messageId);
        Task AddAsync(Message message);
    }

}
