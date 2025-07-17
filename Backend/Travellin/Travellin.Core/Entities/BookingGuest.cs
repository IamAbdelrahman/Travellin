using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class BookingGuest : BaseEntity
    {
        public Guid BookingId { get; set; }

        public Guid? GuestTypeId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        // Navigation properties
        public Booking Booking { get; set; }
        public GuestType? GuestType { get; set; }
    }
}
