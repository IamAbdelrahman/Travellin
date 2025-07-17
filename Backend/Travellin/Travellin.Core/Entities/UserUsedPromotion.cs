namespace Travellin.Travellin.Core.Entities
{
    public class UserUsedPromotion : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid PromotionId { get; set; }

        public Guid? BookingId { get; set; } // Optional, if tied to a specific booking

        public DateTime AppliedAt { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Promotion Promotion { get; set; }
        public Booking? Booking { get; set; }
    }
}
