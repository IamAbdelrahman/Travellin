using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos.HostUpgradeRequests;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;
using Travellin.Core.Enums;

namespace Travellin.Infrastructure.Repositories
{
    class HostUpgradeRequestRepository : GenericRepository<HostUpgradeRequest, string>, IHostUpgradeRequestRepository
    {
        public HostUpgradeRequestRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<PaginatedResult<HostUpgradeRequestDto>> GetAllAsync(HostUpgradeRequestFilterQueryParamsDto queryDto)
        {
            var query = _dbContext.HostUpgradeRequests
                .Include(x => x.FrontPhoto)
                .Include(x => x.BackPhoto)
                .AsQueryable();

            if (!string.IsNullOrEmpty(queryDto.UserId))
                query = query.Where(x => x.UserId == queryDto.UserId);

            if (!string.IsNullOrEmpty(queryDto.Status))
            {
                var status = Enum.Parse<HostUgradeRequestStatus>(queryDto.Status, ignoreCase: true);
                query = query.Where(x => x.Status == status);
            }

            if (!string.IsNullOrEmpty(queryDto.ApprovedBy))
                query = query.Where(x => x.ApprovedBy == queryDto.ApprovedBy);

            if (queryDto.ApprovalDate is not null)
                query = query.Where(x => x.ApprovalDate == queryDto.ApprovalDate);

            if (!string.IsNullOrEmpty(queryDto.DocumentNumber))
                query = query.Where(x => x.DocumentNumber == queryDto.DocumentNumber);

            query = queryDto.Sort switch
            {
                "status" => query.OrderBy(x => x.Status),
                "created_at_asc" => query.OrderBy(x => x.CreatedAt),
                "created_at_desc" => query.OrderByDescending(x => x.CreatedAt
                ),
                "approval_date_asc" => query.OrderBy(x => x.ApprovalDate),
                "approval_date_desc" => query.OrderByDescending(x => x.ApprovalDate),
                _ => query.OrderBy(x => x.CreatedAt),
            };

            var total = await query.CountAsync();
            var items = await query
                .Skip(queryDto.CalcSkippedItems())
                .Take(queryDto.PageSize)
                .Select(x => x.ToDto())
                .ToListAsync();

            return new PaginatedResult<HostUpgradeRequestDto>
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
        public async Task<HostUpgradeRequest?> GetLastRequestByUserIdASync(string userId)
        {
            return await _dbContext.HostUpgradeRequests
            .Include(x => x.FrontPhoto)
            .Include(x => x.BackPhoto)
            .Where(r => r.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
        }

        public async Task<HostUpgradeRequest?> GetPendingRequestByUserIdAsync(string userId)
        {
            return await _dbContext.HostUpgradeRequests
                .Include(x => x.FrontPhoto)
                .Include(x => x.BackPhoto)
                .Where(r => r.UserId == userId && r.Status == HostUgradeRequestStatus.Pending)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
