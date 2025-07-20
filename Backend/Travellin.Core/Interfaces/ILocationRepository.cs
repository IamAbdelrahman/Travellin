using Travellin.Core.Dtos.Locations;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface ILocationRepository : IGenericRepository<Location, int>
    {
        public Task<PaginatedResult<LocationDto>> GetFilteredLocationsAsync(LocationQueryParamsDto queryDto);
    }
}
