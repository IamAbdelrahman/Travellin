using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Dtos.Message;

namespace Travellin.Core.Dtos.Conversation
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }
        public string? User1Name { get; set; } // Added for user display
        public string? User2Name { get; set; } // Added for user display
        public string? PropertyId { get; set; } // Added for property context
        public string? PropertyTitle { get; set; } // Added for property context
        public DateTime CreatedAt { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
