namespace Travellin.Core.Dtos.Properties
{
    public class PropertyDetailsDto : PropertyListItemDto
    {
        public string? Description { get; set; }
        public string? SafteyInfo { get; set; }
        public string? HouseRules { get; set; }
        public string? CancellationPolicy { get; set; }
        public int MaxGuestCount { get; set; }
        public List<PropertySpaceSummaryDto> SpaceSummaries { get; set; }
        public List<PropertySpaceItemSummaryDto> SpaceItemSummaries { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
