using System.ComponentModel.DataAnnotations;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Travellin.Core.Entities
{
    public class Booking : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public Guid GuestId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required, MaxLength(20)]
        public string Status { get; set; } = BookingStatus.Confirmed.ToString(); // "Pending", "Confirmed", "Cancelled", "Completed", "Extended"

        [Required]
        public decimal TotalPrice { get; set; } // USD, including fees

        public Guid? CancellationPolicyId { get; set; }

        // Navigation properties
        public Property Property { get; set; }
        public User Guest { get; set; }
        public CancellationPolicy? CancellationPolicy { get; set; }
        public ICollection<BookingGuest> BookingGuests { get; set; } = new List<BookingGuest>();
        public Payment? Payment { get; set; }
        public Review? Review { get; set; }
    }
}
