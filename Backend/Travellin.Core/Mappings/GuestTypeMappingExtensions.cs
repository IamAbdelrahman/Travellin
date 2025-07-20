using Travellin.Core.Dtos.GuestTypes;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class GuestTypeMappingExtensions
    {
        public static GuestTypesDto ToDto(this GuestType guestType)
        {
            return new GuestTypesDto
            {
                Id = guestType.Id,
                Name = guestType.Name
            };
        }
    }
}
