using Travellin.Core.Dtos.PropertyPhotos;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class PropertyPhotoMappingExtensions
    {

        public static PropertyPhotoDto ToDto(this PropertyPhoto propertyPhoto)
        {
            if (propertyPhoto == null)
                return null;

            return new PropertyPhotoDto
            {
                Id = propertyPhoto.PhotoId,
                PhotoUrl = propertyPhoto.FileUpload.Path.ToFullUrl()
            };
        }
    }
}
