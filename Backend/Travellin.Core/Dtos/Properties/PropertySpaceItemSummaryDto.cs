using Travellin.Core.Dtos.PropertySpaceItemTypes;

namespace Travellin.Core.Dtos.Properties
{
    public class PropertySpaceItemSummaryDto
    {
        public PropertySpaceItemTypeDto ItemType { get; set; }
        public int Quantity { get; set; }
    }
}
