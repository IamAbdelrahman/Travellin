using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Travellin.Core.Dtos.Notifications;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Enums;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Services
{
    public class BookingCompletionService : IBookingCompletionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly ILogger<BookingCompletionService> _logger;
        private readonly TravellinDbContext _dbContext;

        public BookingCompletionService(
            IUnitOfWork unitOfWork,
            INotificationService notificationService,
            ILogger<BookingCompletionService> logger,
            TravellinDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task CompleteExpiredBookingsAsync()
        {
            try
            {
                var now = DateTime.UtcNow;
                var expiredBookings = await _dbContext.Bookings
                    .Include(x => x.User)
                    .Include(x => x.Property)
                    .ThenInclude(x => x.Owner)
                    .Where(x => x.Status == BookingStatus.Confirmed && 
                               x.CheckOut < now)
                    .ToListAsync();

                foreach (var booking in expiredBookings)
                {
                    await MarkBookingAsCompletedAsync(booking.Id);
                }

                _logger.LogInformation("Completed {Count} expired bookings", expiredBookings.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing expired bookings");
                throw;
            }
        }

        public async Task<bool> MarkBookingAsCompletedAsync(string bookingId)
        {
            try
            {
                var booking = await _dbContext.Bookings
                    .Include(x => x.User)
                    .Include(x => x.Property)
                    .ThenInclude(x => x.Owner)
                    .FirstOrDefaultAsync(x => x.Id == bookingId);

                if (booking == null)
                {
                    _logger.LogWarning("Booking {BookingId} not found for completion", bookingId);
                    return false;
                }

                if (booking.Status != BookingStatus.Confirmed)
                {
                    _logger.LogWarning("Booking {BookingId} is not in confirmed status", bookingId);
                    return false;
                }

                booking.Status = BookingStatus.Completed;
                booking.UpdatedAt = DateTime.UtcNow;
                _dbContext.Bookings.Update(booking);
                await _dbContext.SaveChangesAsync();

                // Notify both guest and host about completion
                await NotifyBookingCompletionAsync(booking);

                // Schedule review period start notification
                await NotifyReviewPeriodStartAsync(bookingId);

                _logger.LogInformation("Booking {BookingId} marked as completed", bookingId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking booking {BookingId} as completed", bookingId);
                return false;
            }
        }

        public async Task NotifyReviewPeriodStartAsync(string bookingId)
        {
            try
            {
                var booking = await _dbContext.Bookings
                    .Include(x => x.User)
                    .Include(x => x.Property)
                    .ThenInclude(x => x.Owner)
                    .FirstOrDefaultAsync(x => x.Id == bookingId);

                if (booking == null) return;

                var reviewPeriodStart = booking.CheckOut.AddDays(1);

                // Notify guest about review period
                await _notificationService.NotifyReviewPeriodStartAsync(
                    booking.UserId,
                    new ReviewPeriodNotificationDto
                    {
                        BookingId = booking.Id,
                        PropertyTitle = booking.Property.Title,
                        ReviewPeriodStart = reviewPeriodStart,
                        ReviewPeriodEnd = reviewPeriodStart.AddDays(13), // 14 days total
                        UserType = "Guest"
                    });

                // Notify host about review period
                await _notificationService.NotifyReviewPeriodStartAsync(
                    booking.Property.OwnerId,
                    new ReviewPeriodNotificationDto
                    {
                        BookingId = booking.Id,
                        PropertyTitle = booking.Property.Title,
                        ReviewPeriodStart = reviewPeriodStart,
                        ReviewPeriodEnd = reviewPeriodStart.AddDays(13), // 14 days total
                        UserType = "Host"
                    });

                _logger.LogInformation("Review period notifications sent for booking {BookingId}", bookingId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error notifying review period start for booking {BookingId}", bookingId);
            }
        }

        public async Task NotifyReviewPeriodEndAsync(string bookingId)
        {
            try
            {
                var booking = await _dbContext.Bookings
                    .Include(x => x.User)
                    .Include(x => x.Property)
                    .ThenInclude(x => x.Owner)
                    .FirstOrDefaultAsync(x => x.Id == bookingId);

                if (booking == null) return;

                // Check if reviews were submitted
                var hasGuestReview = await _dbContext.Reviews
                    .AnyAsync(r => r.BookingId == bookingId && r.Type == ReviewType.Guest);

                var hasHostReview = await _dbContext.Reviews
                    .AnyAsync(r => r.BookingId == bookingId && r.Type == ReviewType.Host);

                // Notify guest if no review submitted
                if (!hasGuestReview)
                {
                    await _notificationService.NotifyReviewPeriodEndAsync(
                        booking.UserId,
                        new ReviewPeriodEndNotificationDto
                        {
                            BookingId = booking.Id,
                            PropertyTitle = booking.Property.Title,
                            UserType = "Guest"
                        });
                }

                // Notify host if no review submitted
                if (!hasHostReview)
                {
                    await _notificationService.NotifyReviewPeriodEndAsync(
                        booking.Property.OwnerId,
                        new ReviewPeriodEndNotificationDto
                        {
                            BookingId = booking.Id,
                            PropertyTitle = booking.Property.Title,
                            UserType = "Host"
                        });
                }

                _logger.LogInformation("Review period end notifications sent for booking {BookingId}", bookingId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error notifying review period end for booking {BookingId}", bookingId);
            }
        }

        private async Task NotifyBookingCompletionAsync(Booking booking)
        {
            try
            {
                // Notify guest about booking completion
                await _notificationService.NotifyBookingCompletionAsync(
                    booking.UserId,
                    new BookingCompletionNotificationDto
                    {
                        BookingId = booking.Id,
                        PropertyTitle = booking.Property.Title,
                        CheckOutDate = booking.CheckOut,
                        HostName = booking.Property.Owner?.UserName ?? "Host"
                    });

                // Notify host about booking completion
                await _notificationService.NotifyBookingCompletionAsync(
                    booking.Property.OwnerId,
                    new BookingCompletionNotificationDto
                    {
                        BookingId = booking.Id,
                        PropertyTitle = booking.Property.Title,
                        CheckOutDate = booking.CheckOut,
                        GuestName = booking.User?.UserName ?? "Guest"
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error notifying booking completion for booking {BookingId}", booking.Id);
            }
        }
    }
} 