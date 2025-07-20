using Travellin.Core.Dtos.Locations;
using Travellin.Core.Dtos.PropertyPhotos;
using Travellin.Core.Dtos.PropertyTypes;

namespace Travellin.Core.Dtos.Properties
{
    public class PropertyListItemDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public PropertyOwnerDto Owner { get; set; }
        public LocationDto Location { get; set; }
        public PropertyTypeDto PropertyType { get; set; }
        public List<PropertyPhotoDto>? Photos { get; set; }
        public decimal AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
