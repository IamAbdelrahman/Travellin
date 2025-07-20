using Travellin.Core.Dtos.PropertyTypes;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class PropertyTypeMappingExtensions
    {
        public static PropertyTypeDto ToDto(this PropertyType propertyType)
        {
            return new PropertyTypeDto
            {
                Id = propertyType.Id,
                Name = propertyType.Name,
                Icon = propertyType.Icon
            };
        }
    }
}
