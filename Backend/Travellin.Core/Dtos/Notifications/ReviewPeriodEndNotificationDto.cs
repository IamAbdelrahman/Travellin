namespace Travellin.Core.Dtos.Notifications
{
    public class ReviewPeriodEndNotificationDto
    {
        public string BookingId { get; set; }
        public string PropertyTitle { get; set; }
        public string UserType { get; set; } // "Guest" or "Host"
    }
} 