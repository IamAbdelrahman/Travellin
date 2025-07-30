using Microsoft.Extensions.Logging;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Enums;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Infrastructure.Services
{
    public class CancellationService : ICancellationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentRefundService _paymentRefundService;
        private readonly ILogger<CancellationService> _logger;

        public CancellationService(
            IUnitOfWork unitOfWork, 
            IPaymentRefundService paymentRefundService,
            ILogger<CancellationService> logger)
        {
            _unitOfWork = unitOfWork;
            _paymentRefundService = paymentRefundService;
            _logger = logger;
        }

        public async Task<CancellationResult> CancelBookingAsync(CancellationRequest request)
        {
            try
            {
                var booking = await _unitOfWork.BookingRepository.GetByIdAsync(request.BookingId, 
                    x => x.Property, x => x.Payments);

                if (booking == null)
                {
                    return new CancellationResult
                    {
                        IsSuccessful = false,
                        Message = "Booking not found"
                    };
                }

                // Check if cancellation is allowed
                if (!await CanCancelBookingAsync(request.BookingId, request.CancelledByUserId, request.IsHostCancellation))
                {
                    return new CancellationResult
                    {
                        IsSuccessful = false,
                        Message = "Cancellation not allowed for this booking"
                    };
                }

                // Calculate refund amount based on cancellation policy
                var refundAmount = await CalculateRefundAmountAsync(request.BookingId, DateTime.UtcNow);
                var isWithinCancellationWindow = await IsWithinCancellationWindowAsync(request.BookingId, DateTime.UtcNow);

                // Update booking status
                booking.Status = BookingStatus.Cancelled;
                booking.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.BookingRepository.Update(booking);

                // Process refund if payment was made
                var refundResult = new RefundResult { IsSuccessful = true };
                if (booking.Payments.Any(p => p.Status == PaymentStatus.Successed) && refundAmount > 0)
                {
                    var payment = booking.Payments.First(p => p.Status == PaymentStatus.Successed);
                    var refundRequest = new RefundRequest
                    {
                        PaymentId = payment.Id,
                        Amount = refundAmount,
                        Reason = "requested_by_customer",
                        IsPartialRefund = refundAmount < payment.Amount
                    };

                    refundResult = await _paymentRefundService.ProcessRefundAsync(refundRequest);
                }

                // Restore availability
                await RestoreAvailabilityAsync(booking.Property, booking.CheckIn, booking.CheckOut);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Booking {BookingId} cancelled by user {UserId}, refund amount: {RefundAmount}", 
                    request.BookingId, request.CancelledByUserId, refundAmount);

                return new CancellationResult
                {
                    IsSuccessful = true,
                    Message = refundResult.IsSuccessful ? 
                        $"Booking cancelled successfully. Refund of ${refundAmount:F2} processed." :
                        $"Booking cancelled successfully. Refund processing failed: {refundResult.Message}",
                    RefundAmount = refundAmount,
                    RefundId = refundResult.StripeRefundId,
                    NewBookingStatus = BookingStatus.Cancelled,
                    NewPaymentStatus = refundResult.NewPaymentStatus
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cancellation failed for booking {BookingId}", request.BookingId);
                return new CancellationResult
                {
                    IsSuccessful = false,
                    Message = $"Cancellation failed: {ex.Message}"
                };
            }
        }

        public async Task<CancellationResult> ProcessRefundAsync(string bookingId, decimal refundAmount)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId, x => x.Payments);
            if (booking == null)
            {
                return new CancellationResult
                {
                    IsSuccessful = false,
                    Message = "Booking not found"
                };
            }

            var payment = booking.Payments.FirstOrDefault(p => p.Status == PaymentStatus.Successed);
            if (payment == null)
            {
                return new CancellationResult
                {
                    IsSuccessful = false,
                    Message = "No successful payment found for this booking"
                };
            }

            var refundRequest = new RefundRequest
            {
                PaymentId = payment.Id,
                Amount = refundAmount,
                Reason = "requested_by_customer",
                IsPartialRefund = refundAmount < payment.Amount
            };

            var refundResult = await _paymentRefundService.ProcessRefundAsync(refundRequest);

            return new CancellationResult
            {
                IsSuccessful = refundResult.IsSuccessful,
                Message = refundResult.Message,
                RefundAmount = refundResult.RefundedAmount,
                RefundId = refundResult.StripeRefundId,
                NewPaymentStatus = refundResult.NewPaymentStatus
            };
        }

        public async Task<bool> CanCancelBookingAsync(string bookingId, string userId, bool isHost)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking == null) return false;

            // Admin can cancel any booking
            if (isHost && booking.Property.OwnerId == userId) return true;

            // Guest can cancel their own booking
            if (!isHost && booking.UserId == userId) return true;

            // Check if booking is in a cancellable state
            return booking.Status == BookingStatus.Pending || 
                   booking.Status == BookingStatus.Confirmed;
        }

        public async Task<decimal> CalculateRefundAmountAsync(string bookingId, DateTime cancellationDate)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId, x => x.Property);
            if (booking == null) return 0;

            // Check if within cancellation window
            if (!await IsWithinCancellationWindowAsync(bookingId, cancellationDate))
                return 0;

            // For now, return full amount if within 24 hours of check-in
            var daysUntilCheckIn = (booking.CheckIn - cancellationDate).TotalDays;
            
            if (daysUntilCheckIn >= 1) // More than 24 hours before check-in
            {
                return booking.TotalAmount; // Full refund
            }
            else if (daysUntilCheckIn >= 0) // Within 24 hours
            {
                return booking.TotalAmount * 0.5m; // 50% refund
            }
            
            return 0; // No refund
        }

        public async Task<bool> IsWithinCancellationWindowAsync(string bookingId, DateTime cancellationDate)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking == null) return false;

            // Allow cancellation up to 24 hours before check-in
            var daysUntilCheckIn = (booking.CheckIn - cancellationDate).TotalDays;
            return daysUntilCheckIn >= 0;
        }

        private async Task RestoreAvailabilityAsync(Property property, DateTime checkIn, DateTime checkOut)
        {
            // This method reuses the existing logic from BookingManagementService
            // Implementation would be similar to the existing RestoreAvailabilityAsync method
            await Task.CompletedTask; // Placeholder
        }
    }
} 