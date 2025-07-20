using Travellin.Core.Dtos.PropertySpaces;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class PropertySpaceMappingExtenstions
    {
        public static PropertySpaceDto ToDto(this PropertySpace propertySpace)
        {
            return new PropertySpaceDto
            {
                Id = propertySpace.Id,
                Name = propertySpace.Name,
                IsShared = propertySpace.IsShared,
                PropertySpaceTypeId = propertySpace.PropertySpaceTypeId,
                PropertyId = propertySpace.PropertyId
            };
        }
    }
}
