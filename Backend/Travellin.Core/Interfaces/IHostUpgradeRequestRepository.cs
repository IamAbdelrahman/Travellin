using Travellin.Core.Dtos.HostUpgradeRequests;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IHostUpgradeRequestRepository : IGenericRepository<HostUpgradeRequest, string>
    {
        public Task<PaginatedResult<HostUpgradeRequestDto>> GetAllAsync(HostUpgradeRequestFilterQueryParamsDto queryDto);
        public Task<HostUpgradeRequest?> GetPendingRequestByUserIdAsync(string userId);
        public Task<HostUpgradeRequest?> GetLastRequestByUserIdASync(string userId);
    }
}
