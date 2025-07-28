using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.PropertyGuests;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class PropertyGuestRepository : BaseRepository, IPropertyGuestRepository
    {
        public PropertyGuestRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<PaginatedResult<PropertyGuestDto>> GetByPropertyIdAsync(string propertyId, GetAllQueryDto dto)
        {
            var query = _dbContext.PropertyGuests
                .Include(x => x.GuestType)
                .Where(x => x.PropertyId == propertyId)
                .AsQueryable();

            var total = await query.CountAsync();

            var items = await query
                    .Skip(dto.CalcSkippedItems())
                    .Take(dto.PageSize)
                    .Select(x => x.ToDto())
                    .ToListAsync();

            return new PaginatedResult<PropertyGuestDto>
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

        public async Task<PropertyGuest?> GetByPropertyAndGuestTypeAsync(string propertyId, int guestTypeId)
        {
            return await _dbContext.PropertyGuests
               .Include(x => x.GuestType)
               .Where(x => x.PropertyId == propertyId && x.GuestTypeId == guestTypeId)
               .FirstOrDefaultAsync();
        }
        /////////////////////   GetAllPropertyGuests   /////////////////////
        public async Task<List<PropertyGuest>> GetAllPropertyGuests(string propertyId)
        {
            return await _dbContext.PropertyGuests
                .Include(x => x.GuestType)
                .Where(x => x.PropertyId == propertyId)
                .ToListAsync();
        }

        public void Create(PropertyGuest entity)
        {
            _dbContext.Add(entity);
        }

        public void Update(PropertyGuest entity)
        {
            _dbContext.Update(entity);
        }
        public void Delete(PropertyGuest entity)
        {
            _dbContext.Remove(entity);
        }
    }
}
