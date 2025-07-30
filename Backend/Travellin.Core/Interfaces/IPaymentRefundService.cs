using Travellin.Travellin.Core.Enums;

namespace Travellin.Core.Interfaces
{
    public class RefundRequest
    {
        public string PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public bool IsPartialRefund { get; set; }
    }

    public class RefundResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string? StripeRefundId { get; set; }
        public decimal RefundedAmount { get; set; }
        public PaymentStatus NewPaymentStatus { get; set; }
    }

    public interface IPaymentRefundService
    {
        Task<RefundResult> ProcessRefundAsync(RefundRequest request);
        Task<RefundResult> ProcessPartialRefundAsync(RefundRequest request);
        Task<bool> CanRefundPaymentAsync(string paymentId);
        Task<decimal> GetRefundableAmountAsync(string paymentId);
    }
} 