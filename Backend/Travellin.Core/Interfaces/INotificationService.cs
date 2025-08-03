using Travellin.Core.Dtos.Notifications;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Core.Interfaces
{
    public interface INotificationService
    {
        // Core notification methods
        Task<NotificationDto> CreateNotificationAsync(string userId, NotificationType type, string content, string? relatedEntityId = null, Dictionary<string, object>? metadata = null);
        Task<List<NotificationDto>> GetUserNotificationsAsync(string userId, bool includeRead = false, int page = 1, int pageSize = 20);
        Task<int> GetUnreadCountAsync(string userId);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(string userId);
        Task DeleteNotificationAsync(int notificationId);

        // Booking notifications
        Task NotifyBookingRequestAsync(string hostId, BookingRequestNotificationDto bookingRequest);
        Task NotifyBookingResponseAsync(string guestId, BookingResponseNotificationDto bookingResponse);
        Task NotifyBookingCancellationAsync(string userId, string bookingId, string propertyTitle, bool isHost);
        Task NotifyBookingReminderAsync(string userId, BookingReminderNotificationDto reminder);
        Task NotifyBookingCompletionAsync(string userId, BookingCompletionNotificationDto completion);

        // Payment notifications
        Task NotifyPaymentSuccessAsync(string userId, PaymentNotificationDto payment);
        Task NotifyPaymentFailedAsync(string userId, PaymentNotificationDto payment);
        Task NotifyPaymentPendingAsync(string userId, PaymentNotificationDto payment);
        Task NotifyRefundIssuedAsync(string userId, PaymentNotificationDto refund);

        // Message notifications
        Task NotifyNewMessageAsync(string receiverId, MessageNotificationDto message);
        Task NotifyMessageReadAsync(string senderId, string messageId);

        // Review notifications
        Task NotifyReviewReceivedAsync(string hostId, ReviewNotificationDto review);
        Task NotifyReviewResponseAsync(string guestId, string reviewId, string hostName);
        Task NotifyReviewPeriodStartAsync(string userId, ReviewPeriodNotificationDto reviewPeriod);
        Task NotifyReviewPeriodEndAsync(string userId, ReviewPeriodEndNotificationDto reviewPeriodEnd);

        // Host notifications
        Task NotifyHostUpgradeRequestAsync(string adminId, HostUpgradeNotificationDto upgradeRequest);
        Task NotifyCoHostInvitationAsync(string coHostId, string propertyId, string hostName);

        // Guest notifications
        Task NotifyGuestArrivalAsync(string hostId, GuestArrivalNotificationDto arrival);
        Task NotifyGuestDepartureAsync(string hostId, string bookingId, string guestName);

        // System notifications
        Task NotifyPromotionAsync(string userId, SystemNotificationDto promotion);
        Task NotifyMaintenanceAlertAsync(string userId, SystemNotificationDto maintenance);
        Task NotifySecurityAlertAsync(string userId, SystemNotificationDto security);

        // Bulk notifications
        Task NotifyAllHostsAsync(NotificationType type, string content, Dictionary<string, object>? metadata = null);
        Task NotifyAllGuestsAsync(NotificationType type, string content, Dictionary<string, object>? metadata = null);
    }
} 