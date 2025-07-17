using System.ComponentModel.DataAnnotations;
using Travellin.Travellin.Core.Enums;
namespace Travellin.Travellin.Core.Entities
{
    public class Payment : BaseEntity
    {
        public Guid BookingId { get; set; }

        [Required]
        public decimal Amount { get; set; } // USD

        [Required, MaxLength(20)]
        public string Type { get; set; } = PaymentType.Payment.ToString(); // "Payment", "Payout", "Refund"

        [Required, MaxLength(20)]
        public string Status { get; set; } = PaymentStatus.Failed.ToString(); // "Pending", "Completed", "Failed"

        [MaxLength(100)]
        public string? TransactionId { get; set; } // From Stripe/PayPal

        // Navigation property
        public Booking Booking { get; set; }
    }
}
