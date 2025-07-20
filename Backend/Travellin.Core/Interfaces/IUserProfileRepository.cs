using Travellin.Core.Dtos.UserProfilesDto;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IUserProfileRepository : IBaseRepository
    {
        public Task<PaginatedResult<UserProfileDto>> GetFilteredProfilesAsync(UserProfileQueryParamsDto queryDto);
        public Task<UserProfile> GetByUserId(string userId);
        public Task<UserProfileDto> GetProfileDetailsByUserId(string userId);
        public Task CreateAsync(string userId);
        public void Update(UserProfile userProfile);
    }
}
