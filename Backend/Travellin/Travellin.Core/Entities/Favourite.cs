using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class Favourite : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid PropertyId { get; set; }

        [MaxLength(100)]
        public string? WishlistName { get; set; } // e.g., "Summer Vacation"

        // Navigation properties
        public User User { get; set; }
        public Property Property { get; set; }
    }
}
