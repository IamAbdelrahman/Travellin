using Microsoft.AspNet.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Core.Entities
{
    public class Message: BaseEntity<int>
    {
            [ForeignKey("Conversation")]
            public int ConversationId { get; set; }
            [ForeignKey("Sender")]
            public string SenderId { get; set; }
            [ForeignKey("Receiver")]
            public string ReceiverId { get; set; }
            public string Content { get; set; }
            public string? TranslatedContent { get; set; }
            public bool IsRead { get; set; }
            public DateTime SentAt { get; set; }
            public Conversation Conversation { get; set; }
            public AppUser Sender { get; set; }
            public AppUser Receiver { get; set; }
        }
}
