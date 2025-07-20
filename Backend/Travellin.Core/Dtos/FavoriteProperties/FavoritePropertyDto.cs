using Travellin.Core.Dtos.PropertyPhotos;

namespace Travellin.Core.Dtos.FavoriteProperties
{
    public class FavoritePropertyDto
    {
        public string PropertyId { get; set; }
        public string Title { get; set; }
        public PropertyPhotoDto MainPhoto { get; set; }
    }
}
