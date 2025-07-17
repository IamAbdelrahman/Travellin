using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class CoHostAssignment : BaseEntity
    {
        public Guid HostId { get; set; }

        public Guid CoHostId { get; set; }

        [Required, MaxLength(500)]
        public string Permissions { get; set; } // e.g., "Messaging,Bookings,Calendar"

        // Navigation properties
        public User Host { get; set; }
        public User CoHost { get; set; }
    }
}
