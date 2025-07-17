using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Travellin.Travellin.Core.Enums;
namespace Travellin.Travellin.Core.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }

        [Required, MaxLength(50)]
        public string Type { get; set; } = NotificationType.NewMessage.ToString(); // e.g., "BookingConfirmation", "NewMessage"

        [Required, MaxLength(1000)]
        public string Content { get; set; }

        public bool IsRead { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
