using Travellin.Core.Dtos.PropertyGuests;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class PropertyGuestMappingExtensionts
    {
        public static PropertyGuestDto ToDto(this PropertyGuest propertyGuest)
        {
            return new PropertyGuestDto
            {
                PropertyId = propertyGuest.PropertyId,
                GuestType = propertyGuest.GuestType.ToDto(),
                GuestCount = propertyGuest.GuestCount
            };
        }

        public static PropertyGuest ToEntity(this PropertyGuestCreateDto dto)
        {
            return new PropertyGuest
            {
                PropertyId = dto.PropertyId,
                GuestTypeId = dto.GuestTypeId,
                GuestCount = dto.GuestCount
            };
        }
    }
}


