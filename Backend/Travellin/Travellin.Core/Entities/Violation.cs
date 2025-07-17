using System.ComponentModel.DataAnnotations;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Travellin.Core.Entities
{
    public class Violation : BaseEntity
    {
        public Guid ReportedById { get; set; } // User who reported the violation

        public Guid? ReportedPropertyId { get; set; } // Property involved (optional)

        public Guid? ReportedUserId { get; set; } // User (host) involved (optional)

        [Required, MaxLength(50)]
        public string Type { get; set; } = ViolationType.Other.ToString(); // e.g., "OffPlatformPayment", "Other"

        [Required, MaxLength(2000)]
        public string Description { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; } = ViolationStatus.Pending.ToString(); // e.g., "Pending", "Resolved", "Dismissed"

        [MaxLength(2000)]
        public string? AdminNotes { get; set; }

        public DateTime? ResolvedAt { get; set; }

        // Navigation Properties
        public virtual User ReportedBy { get; set; }
        public virtual Property? ReportedProperty { get; set; }
        public virtual User? ReportedUser { get; set; }
    }
}
