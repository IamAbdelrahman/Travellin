using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Dtos.Properties
{
    public class PropertySearchResultDto : PaginatedResult<PropertyListItemDto>
    {
        public FilterPropertyQueryParamsDto SearchParams { get; set; }
    }
}
