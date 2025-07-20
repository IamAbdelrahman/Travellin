using Travellin.Core.Dtos;
using Travellin.Core.Dtos.FavoriteProperties;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IFavoritePropertyRepository : IBaseRepository
    {
        Task<PaginatedResult<FavoritePropertyDto>> GetAllByUserIdAsync(string userId, GetAllQueryDto queryDto);
        Task CreateAsync(string userId, string propertyId);
        Task DeleteAsync(string userId, string propertyId);
    }
}
