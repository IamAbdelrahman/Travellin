using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.FavoriteProperties
{
    public class CreateFavoritePropertyDto
    {
        [Required]
        public string PropertyId { get; set; }
    }
}
