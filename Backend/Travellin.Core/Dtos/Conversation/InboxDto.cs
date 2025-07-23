using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Core.Dtos.Conversation
{
    public class InboxDto
    {
        public int ConversationId { get; set; }
        public string Participant { get; set; }
        public string? LastMessage { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsUnread { get; set; }
    }

}
