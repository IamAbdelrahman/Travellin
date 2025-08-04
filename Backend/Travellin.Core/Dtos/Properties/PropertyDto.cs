using Travellin.Core.Dtos.PropertyPhotos;

namespace Travellin.Core.Dtos.Properties
{
    public class PropertyDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string OwnerId { get; set; }
        public int PropertyTypeId { get; set; }
        public int LocationId { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? SafteyInfo { get; set; }
        public string? HouseRules { get; set; }
        public string? CancellationPolicy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsInstant { get; set; }
        public List<PropertyPhotoDto> Photos { get; set; }  = new List<PropertyPhotoDto>();
    }
}
