using Travellin.Core.Dtos.Bookings;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Core.Interfaces
{
    public class CancellationRequest
    {
        public string BookingId { get; set; }
        public string CancelledByUserId { get; set; }
        public bool IsHostCancellation { get; set; }
        public string? CancellationReason { get; set; }
        public decimal? RefundAmount { get; set; } // For partial refunds
    }

    public class CancellationResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? RefundId { get; set; }
        public BookingStatus NewBookingStatus { get; set; }
        public PaymentStatus? NewPaymentStatus { get; set; }
    }

    public interface ICancellationService
    {
        Task<CancellationResult> CancelBookingAsync(CancellationRequest request);
        Task<CancellationResult> ProcessRefundAsync(string bookingId, decimal refundAmount);
        Task<bool> CanCancelBookingAsync(string bookingId, string userId, bool isHost);
        Task<decimal> CalculateRefundAmountAsync(string bookingId, DateTime cancellationDate);
        Task<bool> IsWithinCancellationWindowAsync(string bookingId, DateTime cancellationDate);
    }
} 