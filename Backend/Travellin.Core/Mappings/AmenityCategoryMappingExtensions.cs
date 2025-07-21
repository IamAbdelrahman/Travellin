using Travellin.Core.Dtos.AmenityCategories;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class AmenityCategoryMappingExtensions
    {
        public static AmenityCategoryDto ToDto(this AmenityCategory amenityCategory)
        {
            return new AmenityCategoryDto
            {
                Id = amenityCategory.Id,
                Name = amenityCategory.Name
            };
        }
    }
}
