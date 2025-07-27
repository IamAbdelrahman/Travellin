using Travellin.Travellin.Core.Enums;

namespace Travellin.Core.Dtos.Notifications
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public NotificationType Type { get; set; }
        public string? RelatedEntityId { get; set; } // BookingId, MessageId, etc.
        public Dictionary<string, object>? Metadata { get; set; }
    }

    // Booking Notifications
    public class BookingRequestNotificationDto
    {
        public string BookingId { get; set; }
        public string GuestName { get; set; }
        public string PropertyTitle { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalAmount { get; set; }
        public string? GuestMessage { get; set; }
        public int GuestCount { get; set; }
    }

    public class BookingResponseNotificationDto
    {
        public string BookingId { get; set; }
        public string HostName { get; set; }
        public string PropertyTitle { get; set; }
        public string Status { get; set; } // "accepted" | "declined"
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string? HostMessage { get; set; }
    }

    public class BookingReminderNotificationDto
    {
        public string BookingId { get; set; }
        public string PropertyTitle { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string ReminderType { get; set; } // "checkin_tomorrow", "checkin_today", "checkout_tomorrow"
    }

    // Payment Notifications
    public class PaymentNotificationDto
    {
        public string BookingId { get; set; }
        public string PropertyTitle { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; } // "success", "failed", "pending"
        public DateTime PaymentDate { get; set; }
        public string? TransactionId { get; set; }
    }

    // Message Notifications
    public class MessageNotificationDto
    {
        public string MessageId { get; set; }
        public string ConversationId { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }

    // Review Notifications
    public class ReviewNotificationDto
    {
        public string ReviewId { get; set; }
        public string BookingId { get; set; }
        public string PropertyTitle { get; set; }
        public string ReviewerName { get; set; }
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }

    // Host Notifications
    public class HostUpgradeNotificationDto
    {
        public string RequestId { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; } // "pending", "approved", "rejected"
        public DateTime RequestDate { get; set; }
        public string? AdminMessage { get; set; }
    }

    // Guest Notifications
    public class GuestArrivalNotificationDto
    {
        public string BookingId { get; set; }
        public string GuestName { get; set; }
        public string PropertyTitle { get; set; }
        public DateTime CheckIn { get; set; }
        public string? GuestMessage { get; set; }
    }

    // System Notifications
    public class SystemNotificationDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // "promotion", "maintenance", "security"
        public DateTime ExpiresAt { get; set; }
        public string? ActionUrl { get; set; }
    }
} 