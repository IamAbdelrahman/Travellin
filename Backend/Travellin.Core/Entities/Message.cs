using Microsoft.AspNet.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Core.Entities
{
    public class Message: BaseEntity<int>
    {
            public Guid ConversationId { get; set; }
            public Guid SenderId { get; set; }
            public Guid ReceiverId { get; set; }
            public string Content { get; set; }
            public string? TranslatedContent { get; set; }
            public bool IsRead { get; set; }

            public Conversation Conversation { get; set; }
            public AppUser Sender { get; set; }
            public AppUser Receiver { get; set; }
        }
}
