using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos.UserProfilesDto;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    internal class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<UserProfile> GetByUserId(string userId)
        {
            var query =
                _dbContext.UserProfiles.Include(u => u.AppUser)
                  .Include(u => u.Country)
                  .Include(u => u.AppUser)
                  .Include(u => u.Photo)
                  .Where(x => x.UserId == userId);

            var user = await query.FirstOrDefaultAsync();

            if (user is null)
            {
                await CreateAsync(userId);
            }

            return await query.FirstAsync();
        }

        public async Task<UserProfileDto> GetProfileDetailsByUserId(string userId)
        {
            var query = _dbContext.UserProfiles.Include(u => u.AppUser)
                .Include(u => u.Country)
                .Include(u => u.AppUser)
                .ThenInclude(u => u.Roles)
                .Include(u => u.Photo)
                .Where(u => u.UserId == userId)
                .Select(u => u.ToDto());

            var user = await query.FirstOrDefaultAsync();

            if (user is null)
            {
                await CreateAsync(userId);
            }

            return await query.FirstOrDefaultAsync(); ;
        }

        public async Task CreateAsync(string userId)
        {
            var newUserProfile = _dbContext.Add(new UserProfile
            {
                UserId = userId,
            });

            await SaveChangesAsync();
        }

        public void Update(UserProfile userProfile)
        {
            _dbContext.Update(userProfile);
        }

        public async Task<PaginatedResult<UserProfileDto>> GetFilteredProfilesAsync(UserProfileQueryParamsDto queryDto)
        {
            var query = _dbContext.UserProfiles
                .Include(u => u.AppUser)
                    .ThenInclude(u => u.Roles)
                .Include(u => u.Country)
                .Include(u => u.Photo)
                .AsQueryable();


            if (!string.IsNullOrEmpty(queryDto.UserId))
            {
                query = query.Where(x => x.UserId == queryDto.UserId);
            }
            else if (!string.IsNullOrEmpty(queryDto.Email))
            {
                query = query.Where(x => x.AppUser.Email == queryDto.Email);
            }
            else if (!string.IsNullOrEmpty(queryDto.UserName))
            {
                query = query.Where(x => x.AppUser.UserName == queryDto.UserName);
            }

            if (!string.IsNullOrEmpty(queryDto.Role))
            {
                query = query.Where(x => x.AppUser.Roles.Any(r => r.Name == queryDto.Role));
            }


            var total = await query.CountAsync();

            query = query
                .Skip(queryDto.CalcSkippedItems())
                .Take(queryDto.PageSize);

            var items = await query
                .Select(x => x.ToDto())
                .ToListAsync();

            return new PaginatedResult<UserProfileDto>
            {
                Items = items,
                MetaData = new PaginationMetaData
                {
                    Page = queryDto.Page,
                    PageSize = queryDto.PageSize,
                    Total = total
                }
            };
        }
    }
}
