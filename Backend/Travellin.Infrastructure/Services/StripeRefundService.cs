using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Enums;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Infrastructure.Services
{
    public class StripeRefundService : IPaymentRefundService
    {
        private readonly StripeClient _stripeClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StripeRefundService> _logger;

        public StripeRefundService(StripeClient stripeClient, IUnitOfWork unitOfWork, ILogger<StripeRefundService> logger)
        {
            _stripeClient = stripeClient;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<RefundResult> ProcessRefundAsync(RefundRequest request)
        {
            try
            {
                var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(request.PaymentId);
                if (payment == null)
                {
                    return new RefundResult
                    {
                        IsSuccessful = false,
                        Message = "Payment not found"
                    };
                }

                if (payment.Status != PaymentStatus.Successed)
                {
                    return new RefundResult
                    {
                        IsSuccessful = false,
                        Message = "Payment is not in a refundable state"
                    };
                }

                if (string.IsNullOrEmpty(payment.StripePaymentIntentId))
                {
                    return new RefundResult
                    {
                        IsSuccessful = false,
                        Message = "No Stripe payment intent found"
                    };
                }

                var refundService = new RefundService(_stripeClient);
                var refundOptions = new RefundCreateOptions
                {
                    PaymentIntent = payment.StripePaymentIntentId,
                    Amount = (long)(request.Amount * 100), // Convert to cents
                    Reason = request.Reason switch
                    {
                        "requested_by_customer" => RefundReasons.RequestedByCustomer,
                        "duplicate" => RefundReasons.Duplicate,
                        "fraudulent" => RefundReasons.Fraudulent,
                        _ => RefundReasons.RequestedByCustomer
                    }
                };

                var refund = await refundService.CreateAsync(refundOptions);

                // Update payment status
                payment.Status = request.IsPartialRefund ? PaymentStatus.Successed : PaymentStatus.Refunded;
                payment.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.PaymentRepository.Update(payment);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Refund processed successfully for payment {PaymentId}, refund amount: {Amount}", 
                    request.PaymentId, request.Amount);

                return new RefundResult
                {
                    IsSuccessful = true,
                    Message = "Refund processed successfully",
                    StripeRefundId = refund.Id,
                    RefundedAmount = request.Amount,
                    NewPaymentStatus = payment.Status
                };
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe refund failed for payment {PaymentId}", request.PaymentId);
                return new RefundResult
                {
                    IsSuccessful = false,
                    Message = $"Stripe refund failed: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Refund processing failed for payment {PaymentId}", request.PaymentId);
                return new RefundResult
                {
                    IsSuccessful = false,
                    Message = $"Refund processing failed: {ex.Message}"
                };
            }
        }

        public async Task<RefundResult> ProcessPartialRefundAsync(RefundRequest request)
        {
            request.IsPartialRefund = true;
            return await ProcessRefundAsync(request);
        }

        public async Task<bool> CanRefundPaymentAsync(string paymentId)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(paymentId);
            return payment != null && 
                   payment.Status == PaymentStatus.Successed && 
                   !string.IsNullOrEmpty(payment.StripePaymentIntentId);
        }

        public async Task<decimal> GetRefundableAmountAsync(string paymentId)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(paymentId);
            if (payment == null || payment.Status != PaymentStatus.Successed)
                return 0;

            return payment.Amount;
        }
    }
} 