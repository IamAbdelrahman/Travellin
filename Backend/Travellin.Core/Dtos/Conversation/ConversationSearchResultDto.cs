using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Core.Dtos.Conversation
{
    public class ConversationSearchResultDto
    {
        public int ConversationId { get; set; }
        public string Participant { get; set; }
        public string? MatchedMessage { get; set; }
    }
}
