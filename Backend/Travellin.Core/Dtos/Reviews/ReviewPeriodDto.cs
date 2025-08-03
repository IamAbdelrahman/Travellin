namespace Travellin.Core.Dtos.Reviews
{
    public class ReviewPeriodDto
    {
        public string BookingId { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime ReviewPeriodStart { get; set; }
        public DateTime ReviewPeriodEnd { get; set; }
        public bool CanReviewAsGuest { get; set; }
        public bool CanReviewAsHost { get; set; }
        public bool HasGuestReview { get; set; }
        public bool HasHostReview { get; set; }
        public int DaysRemaining { get; set; }
    }
} 