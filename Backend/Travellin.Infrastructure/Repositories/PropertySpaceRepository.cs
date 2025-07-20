using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.PropertySpaces;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class PropertySpaceRepository : GenericRepository<PropertySpace, string>, IPropertySpaceRepository
    {
        public PropertySpaceRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<PaginatedResult<PropertySpaceDto>> GetByPropertyIdAsync(string propertyId, GetAllQueryDto dto)
        {
            var query = _dbContext.PropertySpaces
                .Where(x => x.PropertyId == propertyId)
                .AsQueryable();

            var total = await query.CountAsync();

            var items = await query
                    .Skip(dto.CalcSkippedItems())
                    .Take(dto.PageSize)
                    .Select(x => x.ToDto())
                    .ToListAsync();

            return new PaginatedResult<PropertySpaceDto>
            {
                Items = items,
                MetaData = new PaginationMetaData
                {
                    Total = total,
                    Page = dto.Page,
                    PageSize = dto.PageSize
                }
            };
        }
    }
}
