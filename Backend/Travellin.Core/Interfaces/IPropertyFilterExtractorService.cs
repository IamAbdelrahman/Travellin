using Travellin.Core.Dtos.Properties;

namespace Travellin.Core.Interfaces
{
    public interface IPropertyFilterExtractorService
    {
        Task<FilterPropertyQueryParamsDto> ExtractFiltersAsync(string naturalLanguageQuery);
    }
}
