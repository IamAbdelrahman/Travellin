using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.PropertySpaceItems
{
    public class PropertySpaceItemUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "PropertySpaceItemTypeId must be greater than 1.")]
        public int? PropertySpaceItemTypeId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Qunatity must be greater than 1.")]
        public int? Quantity { get; set; }
    }
}
