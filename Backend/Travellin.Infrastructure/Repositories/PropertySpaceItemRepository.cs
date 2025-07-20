using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.PropertySpaceItems;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class PropertySpaceItemRepository : GenericRepository<PropertySpaceItem, int>, IPropertySpaceItemRepository
    {
        public PropertySpaceItemRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<PaginatedResult<PropertySpaceItemDto>> GetByPropertySpaceIdAsync(string propertySpaceId, GetAllQueryDto dto)
        {
            var query = _dbContext.PropertySpaceItems
                .Where(x => x.PropertySpaceId == propertySpaceId)
                .AsQueryable();

            var total = await query.CountAsync();

            var items = await query
                    .Skip(dto.CalcSkippedItems())
                    .Take(dto.PageSize)
                    .Select(x => x.ToDto())
                    .ToListAsync();

            return new PaginatedResult<PropertySpaceItemDto>
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
