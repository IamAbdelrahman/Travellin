namespace Travellin.Travellin.Core.Entities
{
    public class PropertyAmenity : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public Guid AmenityId { get; set; }

        // Navigation properties
        public Property Property { get; set; }
        public Amenity Amenity { get; set; }
    }
}
