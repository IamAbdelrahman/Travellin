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
        public List<MessageDto> Messages { get; set; }
    }
}
