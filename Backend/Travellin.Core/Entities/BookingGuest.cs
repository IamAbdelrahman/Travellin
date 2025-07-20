namespace Travellin.Core.Entities
{
    public class BookingGuest
    {
        public string BookingId { get; set; }
        public int GuestTypeId { get; set; }
        public int GuestCount { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual GuestType GuestType { get; set; }
    }
}
