using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Dtos.Message;
using Travellin.Core.Entities;

namespace Travellin.Core.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> SendMessageAsync(Message message);
        Task<List<MessageDto>> GetMessagesByConversationIdAsync(int conversationId);
        Task MarkMessageAsReadAsync(int messageId);
        Task<int> GetUnreadCountAsync(string userId);
        Task MarkMessagesAsReadAsync(int conversationId, string userId);
    }

}
