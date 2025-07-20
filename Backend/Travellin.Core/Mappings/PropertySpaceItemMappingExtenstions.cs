using Travellin.Core.Dtos.PropertySpaceItems;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class PropertySpaceItemMappingExtenstions
    {
        public static PropertySpaceItemDto ToDto(this PropertySpaceItem propertySpaceItem)
        {
            return new PropertySpaceItemDto
            {
                Id = propertySpaceItem.Id,
                PropertySpaceItemTypeId = propertySpaceItem.PropertySpaceItemTypeId,
                PropertySpaceId = propertySpaceItem.PropertySpaceId,
                Quantity = propertySpaceItem.Quantity
            };
        }
    }
}
