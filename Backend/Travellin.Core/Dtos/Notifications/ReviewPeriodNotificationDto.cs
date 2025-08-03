using System;

namespace Travellin.Core.Dtos.Notifications
{
    public class ReviewPeriodNotificationDto
    {
        public string BookingId { get; set; }
        public string PropertyTitle { get; set; }
        public DateTime ReviewPeriodStart { get; set; }
        public DateTime ReviewPeriodEnd { get; set; }
        public string UserType { get; set; } // "Guest" or "Host"
    }
} 