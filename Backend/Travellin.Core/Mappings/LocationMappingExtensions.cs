using Travellin.Core.Dtos.Locations;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class LocationMappingExtensions
    {
        public static LocationDto ToDto(this Location location)
        {
            return new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                CountryId = location.CountryId
            };
        }
    }
}
