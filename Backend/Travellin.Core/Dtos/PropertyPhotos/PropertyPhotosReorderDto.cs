using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.PropertyPhotos
{
    public class PropertyPhotosReorderDto
    {
        [Required]
        public string PropertyId { get; set; }

        [Required]
        public List<string> PhotoIds { get; set; }
    }
}
