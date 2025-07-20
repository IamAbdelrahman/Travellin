using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos.Locations;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class LocationRepository : GenericRepository<Location, int>, ILocationRepository
    {
        public LocationRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<PaginatedResult<LocationDto>> GetFilteredLocationsAsync(LocationQueryParamsDto queryDto)
        {
            // Handle ID query separately to avoid unnecessary count/offset
            if (queryDto.Id.HasValue)
            {
                var location = await _dbContext.Locations
                    .Where(x => x.Id == queryDto.Id.Value)
                    .Select(p => p.ToDto())
                    .FirstOrDefaultAsync();

                return new()
                {
                    Items = location != null ? new List<LocationDto> { location } : new List<LocationDto>(),
                    MetaData = new PaginationMetaData
                    {
                        Total = location != null ? 1 : 0,
                        Page = 1,
                        PageSize = 1
                    }
                };
            }

            // Original query for non-ID cases
            var query = _dbContext.Locations.AsQueryable();

            if (!string.IsNullOrEmpty(queryDto.Name))
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{queryDto.Name}%"));

            var total = await query.CountAsync();
            var items = await query
                .Skip(queryDto.CalcSkippedItems())
                .Take(queryDto.PageSize)
                .Select(p => p.ToDto())
                .ToListAsync();

            return new()
            {
                Items = items,
                MetaData = new PaginationMetaData
                {
                    Total = total,
                    Page = queryDto.Page,
                    PageSize = queryDto.PageSize
                }
            };
        }
    }
}
