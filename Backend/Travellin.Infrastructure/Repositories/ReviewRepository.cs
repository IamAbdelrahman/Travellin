using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Reviews;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class ReviewRepository : GenericRepository<Review, string>, IReviewRepository
    {
        public ReviewRepository(TravellinDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PaginatedResult<ReviewDto>> GetPropertyReviews(string propertyId, GetAllQueryDto dto)
        {
            var query = _dbContext.Reviews
                .Include(x => x.Booking)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.UserProfile)
                .ThenInclude(x => x.Photo)
                .Include(x => x.Booking)
                .ThenInclude(x => x.Property)
                .Where(x => x.Booking.PropertyId == propertyId)
                .AsQueryable();

            var total = await query.CountAsync();

            var items = await query
                .Skip(dto.CalcSkippedItems())
                .Take(dto.PageSize)
                .Select(x => x.ToDto())
                .ToListAsync();

            return new PaginatedResult<ReviewDto>
            {
                Items = items,
                MetaData = new PaginationMetaData
                {
                    Page = dto.Page,
                    PageSize = dto.PageSize,
                    Total = total,
                }
            };
        }

        public async Task<ReviewDto?> GetReviewDetails(string reviewId)
        {
            var query = _dbContext.Reviews
            .Include(x => x.Booking)
            .ThenInclude(x => x.User)
            .ThenInclude(x => x.UserProfile)
            .ThenInclude(x => x.Photo)
            .Include(x => x.Booking)
            .ThenInclude(x => x.Property)
            .Where(x => x.Id == reviewId)
            .AsQueryable();

            return await query.Select(x => x.ToDto()).FirstOrDefaultAsync();
        }
    }
}