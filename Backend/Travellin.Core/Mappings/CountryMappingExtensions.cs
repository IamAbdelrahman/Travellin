using Travellin.Core.Dtos.Countires;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class CountryMappingExtensions
    {
        public static CountryDto ToDto(this Country country)
        {
            return new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
                RegionId = country.RegionId
            };
        }
    }
}
