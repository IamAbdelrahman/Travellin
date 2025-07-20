using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.PropertySpaces
{
    public class PropertySpaceUpdateDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "PropertySpaceTypeId must be greater than 1.")]
        public int? PropertySpaceTypeId { get; set; }
        public bool? IsShared { get; set; }
    }
}
