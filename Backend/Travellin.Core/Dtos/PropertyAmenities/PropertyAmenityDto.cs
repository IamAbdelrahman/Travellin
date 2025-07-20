using Travellin.Core.Dtos.Amenities;

namespace Travellin.Core.Dtos.PropertyAmenities
{
    public class PropertyAmenityDto
    {
        public string PropertyId { get; set; }
        public AmenityDto Amenity { get; set; }
    }
}
