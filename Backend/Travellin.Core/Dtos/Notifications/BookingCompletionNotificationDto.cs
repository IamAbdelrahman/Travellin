using System;

namespace Travellin.Core.Dtos.Notifications
{
    public class BookingCompletionNotificationDto
    {
        public string BookingId { get; set; }
        public string PropertyTitle { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string HostName { get; set; }
        public string GuestName { get; set; }
    }
} 