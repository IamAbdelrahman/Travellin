using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class Message : BaseEntity
    {
        public Guid ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        [Required, MaxLength(2000)]
        public string Content { get; set; }
        [MaxLength(2000)]
        public string? TranslatedContent { get; set; }
        public bool IsRead { get; set; }

        // Navigation properties
        public Conversation Conversation { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
