using Travellin.Core.Dtos.FavoriteProperties;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class FavoritePropertyMappingExtensions
    {

        public static FavoriteProperty ToEntity(this CreateFavoritePropertyDto dto)
        {
            return new FavoriteProperty
            {
                PropertyId = dto.PropertyId

            };
        }


        public static FavoritePropertyDto ToDto(this FavoriteProperty entity)
        {
            return new FavoritePropertyDto
            {
                PropertyId = entity.PropertyId,
                Title = entity.Property.Title,
                MainPhoto = entity.Property.PropertyPhotos.OrderBy(x => x.TouchedAt).FirstOrDefault().ToDto()

            };

        }
    }
}
