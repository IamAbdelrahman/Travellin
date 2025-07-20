using Travellin.Core.Dtos.PropertySpaceItemTypes;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class PropertySpaceItemTypeMappingExtenstions
    {
        public static PropertySpaceItemTypeDto ToDto(this PropertySpaceItemType propertySpaceItemType)
        {
            return new PropertySpaceItemTypeDto
            {
                Id = propertySpaceItemType.Id,
                Name = propertySpaceItemType.Name,
                PropertySpaceTypeId = propertySpaceItemType.PropertySpaceTypeId
            };
        }
    }
}
