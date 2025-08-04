using Microsoft.Extensions.Logging;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Dtos.Notifications;
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
        private readonly INotificationService _notificationService;
        private readonly ILogger<CancellationService> _logger;

        public CancellationService(
            IUnitOfWork unitOfWork, 
            IPaymentRefundService paymentRefundService,
            INotificationService notificationService,
            ILogger<CancellationService> logger)
        {
            _unitOfWork = unitOfWork;
            _paymentRefundService = paymentRefundService;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task<CancellationResult> CancelBookingAsync(CancellationRequest request)
        {
            try
            {
                _logger.LogInformation("Starting cancellation process for booking {BookingId} by user {UserId}", 
                    request.BookingId, request.CancelledByUserId);

                var booking = await _unitOfWork.BookingRepository.GetByIdAsync(request.BookingId, 
                    x => x.Property, x => x.Payments, x => x.User, x => x.Property.Owner);

                if (booking == null)
                {
                    _logger.LogWarning("Booking {BookingId} not found", request.BookingId);
                    return new CancellationResult
                    {
                        IsSuccessful = false,
                        Message = "Booking not found"
                    };
                }

                // Check if booking is already cancelled or completed
                if (booking.Status == BookingStatus.Cancelled)
                {
                    return new CancellationResult
                    {
                        IsSuccessful = false,
                        Message = "Booking is already cancelled"
                    };
                }

                if (booking.Status == BookingStatus.Completed)
                {
                    return new CancellationResult
                    {
                        IsSuccessful = false,
                        Message = "Cannot cancel completed booking"
                    };
                }

                // Check if cancellation is allowed
                if (!await CanCancelBookingAsync(request.BookingId, request.CancelledByUserId, request.IsHostCancellation))
                {
                    _logger.LogWarning("User {UserId} not authorized to cancel booking {BookingId}", 
                        request.CancelledByUserId, request.BookingId);
                    return new CancellationResult
                    {
                        IsSuccessful = false,
                        Message = "Cancellation not allowed for this booking"
                    };
                }

                // Check if within cancellation window
                var isWithinCancellationWindow = await IsWithinCancellationWindowAsync(request.BookingId, DateTime.UtcNow);
                _logger.LogInformation("Cancellation window check for booking {BookingId}: {IsWithinWindow}", 
                    request.BookingId, isWithinCancellationWindow);
                
                if (!isWithinCancellationWindow)
                {
                    _logger.LogWarning("Cancellation window expired for booking {BookingId}", request.BookingId);
                    return new CancellationResult
                    {
                        IsSuccessful = false,
                        Message = "Cancellation window has expired"
                    };
                }

                // Calculate refund amount based on cancellation policy
                var refundAmount = await CalculateRefundAmountAsync(request.BookingId, DateTime.UtcNow);
                _logger.LogInformation("Calculated refund amount: {RefundAmount} for booking {BookingId}", 
                    refundAmount, request.BookingId);

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
                        Reason = request.IsHostCancellation ? "requested_by_host" : "requested_by_customer",
                        IsPartialRefund = refundAmount < payment.Amount
                    };

                    refundResult = await _paymentRefundService.ProcessRefundAsync(refundRequest);
                }

                // Restore availability
                await RestoreAvailabilityAsync(booking.Property, booking.CheckIn, booking.CheckOut);

                await _unitOfWork.SaveChangesAsync();

                // Notifications are now handled by BookingManagementService to prevent duplicates
                // await NotifyCancellationAsync(booking, request.IsHostCancellation, refundAmount, refundResult);

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
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId, x => x.Property);
            if (booking == null) return false;

            // Check if booking is in a cancellable state
            if (booking.Status != BookingStatus.Pending && booking.Status != BookingStatus.Confirmed)
                return false;

            // Host can cancel bookings for their own properties
            if (isHost && booking.Property.OwnerId == userId) return true;

            // Guest can cancel their own booking
            if (!isHost && booking.UserId == userId) return true;

            return false;
        }

        public async Task<decimal> CalculateRefundAmountAsync(string bookingId, DateTime cancellationDate)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId, x => x.Property);
            if (booking == null) return 0;

            // Check if within cancellation window
            if (!await IsWithinCancellationWindowAsync(bookingId, cancellationDate))
                return 0;

            var daysUntilCheckIn = (booking.CheckIn - cancellationDate).TotalDays;
            var daysUntilCheckOut = (booking.CheckOut - cancellationDate).TotalDays;
            var totalAmount = booking.TotalAmount;

            // Cancellation policy:
            // - More than 7 days before check-in: 100% refund
            // - 3-7 days before check-in: 75% refund
            // - 1-3 days before check-in: 50% refund
            // - Less than 24 hours before check-in: 25% refund
            // - After check-in but before check-out: 10% refund (cleaning fee)
            // - After check-out: No refund

            if (daysUntilCheckIn > 7)
            {
                return totalAmount; // 100% refund
            }
            else if (daysUntilCheckIn > 3)
            {
                return totalAmount * 0.75m; // 75% refund
            }
            else if (daysUntilCheckIn > 1)
            {
                return totalAmount * 0.50m; // 50% refund
            }
            else if (daysUntilCheckIn > 0)
            {
                return totalAmount * 0.25m; // 25% refund
            }
            else if (daysUntilCheckOut > 0)
            {
                return totalAmount * 0.10m; // 10% refund (cleaning fee) for cancellations after check-in
            }
            
            return 0; // No refund for past bookings
        }

        public async Task<bool> IsWithinCancellationWindowAsync(string bookingId, DateTime cancellationDate)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking == null) 
            {
                return false;
            }

            // For pending bookings: allow cancellation anytime
            if (booking.Status == BookingStatus.Pending)
            {
                return true;
            }

            // For confirmed bookings: allow cancellation up to check-out time (not just check-in)
            if (booking.Status == BookingStatus.Confirmed)
            {
                var daysUntilCheckOut = (booking.CheckOut - cancellationDate).TotalDays;
                var canCancel = daysUntilCheckOut >= 0;
                
                return canCancel; // Allow cancellation up to check-out time
            }

            // For other statuses: no cancellation allowed
            return false;
        }

        private async Task RestoreAvailabilityAsync(Property property, DateTime checkIn, DateTime checkOut)
        {
            var availabilities = await _unitOfWork.PropertyAvailabilityRepository.GetAllAsync(
                x => x.PropertyId == property.Id && 
                     x.StartDate <= checkOut && 
                     x.EndDate >= checkIn);

            for (var date = checkIn.Date; date < checkOut.Date; date = date.AddDays(1))
            {
                var availability = availabilities.FirstOrDefault(a => 
                    a.StartDate <= date && a.EndDate >= date);
                if (availability != null)
                {
                    // Check if there are any other active bookings for this date
                    var conflictingBookings = await _unitOfWork.BookingRepository
                        .GetAllAsync(new GetAllBookingsQueryParamsDto());

                    var hasConflictingBookings = conflictingBookings.Items.Any(b => 
                        b.Property.Id == property.Id && 
                        b.Status != "Cancelled" && 
                        b.Status != "Declined" &&
                        b.CheckIn <= date && b.CheckOut > date);

                    if (!hasConflictingBookings)
                    {
                        availability.IsAvailable = true;
                        _unitOfWork.PropertyAvailabilityRepository.Update(availability);
                    }
                }
            }
        }

        private async Task NotifyCancellationAsync(Booking booking, bool isHostCancellation, decimal refundAmount, RefundResult refundResult)
        {
            try
            {
                // Notify guest about cancellation
                await _notificationService.NotifyBookingCancellationAsync(
                    booking.UserId, 
                    booking.Id, 
                    booking.Property.Title, 
                    false);

                // Notify host about cancellation
                await _notificationService.NotifyBookingCancellationAsync(
                    booking.Property.OwnerId, 
                    booking.Id, 
                    booking.Property.Title, 
                    true);

                // Notify about refund if processed successfully
                if (refundResult.IsSuccessful && refundAmount > 0)
                {
                    await _notificationService.NotifyRefundIssuedAsync(
                        booking.UserId,
                        new PaymentNotificationDto
                        {
                            BookingId = booking.Id,
                            Amount = refundAmount,
                            Currency = "USD",
                            Status = "refunded",
                            PropertyTitle = booking.Property.Title
                        });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send cancellation notifications for booking {BookingId}", booking.Id);
            }
        }
    }
} 