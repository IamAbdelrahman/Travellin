using Travellin.Core.Dtos.Regions;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class RegionMappingExtensions
    {
        public static RegionDto ToDto(this Region region)
        {
            return new RegionDto
            {
                Id = region.Id,
                Name = region.Name
            };
        }
    }
}
