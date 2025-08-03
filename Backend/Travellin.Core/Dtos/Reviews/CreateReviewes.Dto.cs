using Travellin.Travellin.Core.Enums;

namespace Travellin.Core.Dtos.Reviews
{
    public class CreateReviewDto
    {
        public string BookingId { get; set; }
        public string? UserId { get; set; } // Optional - will be set by controller from JWT token
        public string Comment { get; set; }
        public decimal Cleanliness { get; set; }
        public decimal Accuracy { get; set; }
        public decimal CheckIn { get; set; }
        public decimal Communication { get; set; }
        public decimal Location { get; set; }
        public decimal Value { get; set; }
        
        // Enhanced review system fields
        public ReviewType Type { get; set; }
        public bool IsAnonymous { get; set; } = false;
    }
}