using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class Amenity : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } // e.g., "WiFi", "Pool"

        public Guid? AmenityCategoryId { get; set; }

        // Navigation properties
        public AmenityCategory? Category { get; set; }
        public ICollection<PropertyAmenity> Properties { get; set; } = new List<PropertyAmenity>();
    }
}
