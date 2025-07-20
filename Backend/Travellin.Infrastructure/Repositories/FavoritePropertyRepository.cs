using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.FavoriteProperties;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class FavoritePropertyRepository : BaseRepository, IFavoritePropertyRepository
    {
        public FavoritePropertyRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task CreateAsync(string userId, string propertyId)
        {
            var userExists = await _dbContext.Users.AnyAsync(u => u.Id == userId);
            var propertyExists = await _dbContext.Properties.AnyAsync(p => p.Id == propertyId);

            if (!userExists || !propertyExists)
            {
                throw new ValidationException("Invalid user ID or property ID.");
            }

            var alreadyFavorited = await _dbContext.FavoriteProperties
                .AnyAsync(f => f.UserId == userId && f.PropertyId == propertyId);

            if (alreadyFavorited)
            {
                throw new ConflictException("This property is already favorited by the user.");
            }

            var entity = new FavoriteProperty { UserId = userId, PropertyId = propertyId };
            _dbContext.FavoriteProperties.Add(entity);
        }


        public async Task DeleteAsync(string UserId, string PropertyId)
        {
            var entity = await _dbContext.FavoriteProperties
                .FirstOrDefaultAsync(f => f.UserId == UserId && f.PropertyId == PropertyId);

            if (entity != null)
            {
                _dbContext.FavoriteProperties.Remove(entity);
            }
        }


        public async Task<PaginatedResult<FavoritePropertyDto>> GetAllByUserIdAsync(string userId, GetAllQueryDto queryDto)
        {
            var query = _dbContext.FavoriteProperties
                .Include(x => x.Property)
                .ThenInclude(x => x.PropertyPhotos)
                .ThenInclude(x => x.FileUpload)
                .Where(x => x.UserId == userId).AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip(queryDto.CalcSkippedItems())
                .Take(queryDto.PageSize)
                .Select(x => x.ToDto())
                .ToListAsync();

            return new PaginatedResult<FavoritePropertyDto>
            {
                Items = items,
                MetaData = new PaginationMetaData
                {
                    Page = queryDto.Page,
                    PageSize = queryDto.PageSize,
                    Total = totalCount
                }
            };
        }
    }
}

