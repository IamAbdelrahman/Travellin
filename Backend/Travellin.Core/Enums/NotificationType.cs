namespace Travellin.Travellin.Core.Enums
{
    public enum NotificationType
    {
        // Booking Notifications
        BookingRequest,
        BookingConfirmed,
        BookingDeclined,
        BookingCancelled,
        BookingReminder,
        GuestArrival,
        GuestDeparture,
        
        // Payment Notifications
        PaymentReceived,
        PaymentFailed,
        PaymentPending,
        RefundIssued,
        
        // Message Notifications
        NewMessage,
        MessageRead,
        
        // Review Notifications
        ReviewReceived,
        ReviewResponse,
        
        // Host Notifications
        HostUpgradeRequest,
        CoHostInvitation,
        PropertyVerification,
        
        // Guest Notifications
        BookingConfirmation,
        CancellationPolicy,
        HouseRules,
        
        // System Notifications
        PromotionAvailable,
        ViolationReported,
        MaintenanceAlert,
        SecurityAlert
    }
}
