using Travellin.Core.Dtos.PropertyAmenities;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class PropertyAmenityMappingExtensions
    {
        public static PropertyAmenityDto ToDto(this PropertyAmenity propertyAmenity)
        {
            return new PropertyAmenityDto
            {
                PropertyId = propertyAmenity.PropertyId,
                Amenity = propertyAmenity.Amenity.ToDto()
            };
        }
        public static PropertyAmenity ToEntity (this PropertyAmenityCreateDto dto)
        {
            return new PropertyAmenity
            {
                PropertyId = dto.PropertyId,
                AmenityId = dto.Amenity?.Id ?? 0

            };
        }
    }
}
