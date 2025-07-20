using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.PropertySpaces
{
    public class PropertySpaceCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PropertySpaceTypeId must be greater than 1.")]
        public int PropertySpaceTypeId { get; set; }
        [Required]
        public string PropertyId { get; set; }
        [Required]
        public bool IsShared { get; set; }
    }
}
