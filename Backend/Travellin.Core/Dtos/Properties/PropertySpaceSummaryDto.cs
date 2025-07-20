using Travellin.Core.Dtos.PropertySpaceTypes;

namespace Travellin.Core.Dtos.Properties
{
    public class PropertySpaceSummaryDto
    {
        public PropertySpaceTypeDto SpaceType { get; set; }
        public int Count { get; set; }
        public bool IsShared { get; set; }
    }
}
