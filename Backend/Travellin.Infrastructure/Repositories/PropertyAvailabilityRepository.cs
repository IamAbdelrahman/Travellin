using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos.PropertyAvailabilities;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;
using System.Linq.Expressions;

namespace Travellin.Infrastructure.Repositories
{
    class PropertyAvailabilityRepository : GenericRepository<PropertyAvailability, int>, IPropertyAvailabilityRepository
    {
        public PropertyAvailabilityRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<List<PropertyAvailability>> GetAllAsync(Expression<Func<PropertyAvailability, bool>> filter)
        {
            return await _dbContext.PropertyAvailabilities.Where(filter).ToListAsync();
        }
        public async Task<PaginatedResult<PropertyAvailabilityDto>> GetByPropertyIdAsync(string propertyId, PropertyAvailabilityQueryParamsDto queryDto)
        {
            var query = _dbContext.PropertyAvailabilities
               .Where(x => x.PropertyId == propertyId)
               .AsQueryable();

            if (queryDto.StartDate.HasValue)
            {
                var checkInDate = queryDto.StartDate.Value.ToDateTime(TimeOnly.MinValue);

                if (queryDto.EndDate.HasValue)
                {
                    var checkOutDate = queryDto.EndDate.Value.ToDateTime(TimeOnly.MinValue);

                    query = query.Where(x =>
                            x.IsAvailable &&
                            x.StartDate <= checkOutDate &&  // Availability starts before or on checkout
                            x.EndDate >= checkInDate
                    );
                }
                else
                {
                    // Only check-in date provided
                    query = query.Where(x =>
                            x.IsAvailable &&
                            x.StartDate <= checkInDate &&
                            x.EndDate >= checkInDate
                    );
                }
            }

            var total = await query.CountAsync();

            var items = await query
                    .Skip(queryDto.CalcSkippedItems())
                    .Take(queryDto.PageSize)
                    .Select(x => x.ToDto())
                    .ToListAsync();

            return new PaginatedResult<PropertyAvailabilityDto>
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

        public void Delete(PropertyAvailability propertyAvailability)
        {
            _dbContext.Remove(propertyAvailability);
        }
    }
}
