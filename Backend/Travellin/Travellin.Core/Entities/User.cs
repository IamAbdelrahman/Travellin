using System.ComponentModel.DataAnnotations;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Travellin.Core.Entities
{
    public class User : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string Email { get; set; } // Unique

        [Required]
        public string PasswordHash { get; set; }

        [Required, MaxLength(20)]
        public string Role { get; set; } = UserRole.Guest.ToString(); // "Guest", "Host", "CoHost"


        [MaxLength(500)]
        public string? ProfilePictureUrl { get; set; }

        [MaxLength(1000)]
        public string? About { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; } // Revealed post-booking

        [MaxLength(10)]
        public string? Language { get; set; } // For auto-translation

        public bool IsVerified { get; set; }

        // Navigation properties
        public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> ReviewsGiven { get; set; } = new List<Review>();
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public ICollection<CoHostAssignment> HostAssignments { get; set; } = new List<CoHostAssignment>();
        public ICollection<CoHostAssignment> CoHostAssignments { get; set; } = new List<CoHostAssignment>();
        public ICollection<Violation> ViolationsReported { get; set; } = new List<Violation>(); // Violations this user reported
        public ICollection<Violation> ViolationsAgainst { get; set; } = new List<Violation>(); // Violations against this user

    }
}
