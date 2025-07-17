using System.ComponentModel.DataAnnotations;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Travellin.Core.Entities
{
    public class GuestType : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Type { get; set; } = TypeGuest.Adult.ToString(); // e.g., "Adult", "Child"

        // Navigation property
        public ICollection<BookingGuest> BookingGuests { get; set; } = new List<BookingGuest>();
    }
}
