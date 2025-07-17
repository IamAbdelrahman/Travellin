using System.ComponentModel.DataAnnotations;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Travellin.Core.Entities
{
    public class PropertyAvailability : BaseEntity
    {
        public Guid PropertyId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(200)]
        public string? Reason { get; set; } = AvailabilityStatus.Available.ToString(); // e.g., "Booked", "HostBlocked"

        // Navigation property
        public Property Property { get; set; }
    }
}
