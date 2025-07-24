using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Reviews;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review, string>
    {
        public Task<PaginatedResult<ReviewDto>> GetPropertyReviews(string propertyId, GetAllQueryDto dto);
        public Task<ReviewDto?> GetReviewDetails(string reviewId);
    }
}