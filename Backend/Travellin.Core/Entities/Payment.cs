using Travellin.Travellin.Core.Enums;
namespace Travellin.Core.Entities
{

    public class Payment : BaseEntity<string>
    {
        public string BookingId { get; set; }
        public string StripeSessionId { get; set; }
        public string? StripePaymentIntentId { get; set; }
        public string? HostStripeAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Booking Booking { get; set; }

    }
}
