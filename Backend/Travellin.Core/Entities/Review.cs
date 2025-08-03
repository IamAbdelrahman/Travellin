using Travellin.Travellin.Core.Enums;

namespace Travellin.Core.Entities
{
    public class Review : BaseEntity<string>
    {
        public string BookingId { get; set; }
        public string Comment { get; set; }
        public decimal Cleanliness { get; set; }
        public decimal Accuracy { get; set; }
        public decimal CheckIn { get; set; }
        public decimal Communication { get; set; }
        public decimal Location { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // New fields for enhanced review system
        public ReviewType Type { get; set; } // Guest or Host review
        public ReviewStatus Status { get; set; } = ReviewStatus.Pending;
        public DateTime? ReviewPeriodStart { get; set; }
        public DateTime? ReviewPeriodEnd { get; set; }
        public bool IsPublic { get; set; } = true;
        public bool IsAnonymous { get; set; } = false;
        
        // Navigation properties
        public virtual Booking Booking { get; set; }
    }
}
