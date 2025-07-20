using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.PropertySpaceItems
{
    public class PropertySpaceItemCreateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PropertySpaceItemTypeId must be greater than 1.")]
        public int PropertySpaceItemTypeId { get; set; }
        [Required]
        public string PropertySpaceId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Qunatity must be greater than 1.")]
        public int Quantity { get; set; }
    }
}
