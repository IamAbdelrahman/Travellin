using Travellin.Core.Dtos.Amenities;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class AmenityMappingExtenstions
    {
        public static AmenityDto ToDto(this Amenity amenity)
        {
            return new AmenityDto
            {
                Id = amenity.Id,
                Name = amenity.Name,
                Icon = amenity?.Icon,
                CategoryId = amenity.CategoryId
            };
        }
    }
}
