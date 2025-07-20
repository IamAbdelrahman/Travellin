using Travellin.Core.Dtos.PropertySpaceTypes;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class PropertySpaceTypeMappingExtensionts
    {
        public static PropertySpaceTypeDto ToDto(this PropertySpaceType propertySpaceType)
        {
            return new PropertySpaceTypeDto
            {
                Id = propertySpaceType.Id,
                Name = propertySpaceType.Name
            };
        }
    }
}
