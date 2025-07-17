using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class AmenityCategory : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } // e.g., "Basics", "Luxury"

        // Navigation property
        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
    }
}
