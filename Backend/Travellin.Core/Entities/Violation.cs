using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Core.Entities
{
    public class Violation : BaseEntity<int>
    {
        public Guid ReportedById { get; set; } // User who reported the violation

        public Guid? ReportedPropertyId { get; set; } // Property involved (optional)

        public Guid? ReportedUserId { get; set; } // User (host) involved (optional)

        public string Name { get; set; } = ViolationType.Other.ToString(); // e.g., "OffPlatformPayment", "Other"

        public string Description { get; set; }

        public string Status { get; set; } = ViolationStatus.Pending.ToString(); // e.g., "Pending", "Resolved", "Dismissed"

        public string? AdminNotes { get; set; }

        public DateTime? ResolvedAt { get; set; }
        public virtual AppUser ReportedBy { get; set; }
        public virtual Property? ReportedProperty { get; set; }
        public virtual AppUser? ReportedUser { get; set; }
    }
}
