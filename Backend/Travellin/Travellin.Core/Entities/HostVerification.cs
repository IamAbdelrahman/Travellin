using System.ComponentModel.DataAnnotations;
using Travellin.Travellin.Core.Enums;
namespace Travellin.Travellin.Core.Entities
{
    public class HostVerification : BaseEntity
    {
        public Guid UserId { get; set; } // Host being verified

        [Required, MaxLength(50)]
        public string VerificationType { get; set; } // e.g., "ID", "BackgroundCheck"

        [Required, MaxLength(50)]
        public string Status { get; set; } = AccountStatus.Active.ToString(); // "Pending", "Active", "Blocked"

        [MaxLength(500)]
        public string? DocumentUrl { get; set; } // Link to uploaded document

        // Navigation property
        public User User { get; set; }
    }
}
