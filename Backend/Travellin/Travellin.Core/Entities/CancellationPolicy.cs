using System.ComponentModel.DataAnnotations;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Travellin.Core.Entities
{
    public class CancellationPolicy : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Type { get; set; } = CancellationType.Flexible.ToString(); // e.g., "Flexible", "Strict"

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        // Navigation property
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
