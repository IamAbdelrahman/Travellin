using Travellin.Core.Dtos.PropertyAvailabilities;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class PropertyAvailabilityMappingExtenstions
    {
        public static PropertyAvailabilityDto ToDto(this PropertyAvailability propertyAvailability)
        {
            return new PropertyAvailabilityDto
            {
                Id = propertyAvailability.Id,
                StartDate = propertyAvailability.StartDate,
                EndDate = propertyAvailability.EndDate,
                PropertyId = propertyAvailability.PropertyId
            };
        }
        public static PropertyAvailability ToEntity (this PropertyAvailabilityCreateDto dto)
        {
            return new PropertyAvailability 
            {
                PropertyId = dto.PropertyId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsAvailable = true
            };

        }
    }
}
