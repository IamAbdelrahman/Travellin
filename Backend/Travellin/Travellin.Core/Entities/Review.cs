using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class Review : BaseEntity
    {
        public Guid BookingId { get; set; }
        public Guid ReviewerId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        public bool IsVisible { get; set; }

        // Navigation properties
        public Booking Booking { get; set; }
        public User Reviewer { get; set; }
    }
}
