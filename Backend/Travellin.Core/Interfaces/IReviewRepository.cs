using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Reviews;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Enums;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review, string>
    {
        public Task<PaginatedResult<ReviewDto>> GetPropertyReviews(string propertyId, GetAllQueryDto dto);
        public Task<ReviewDto?> GetReviewDetails(string reviewId);
        
        // Enhanced review system methods
        public Task<ReviewPeriodDto?> GetReviewPeriodAsync(string bookingId, string userId);
        public Task<bool> CanReviewAsync(string bookingId, string userId, ReviewType type);
        public Task<IEnumerable<ReviewDto>> GetPropertyReviewsAsync(string propertyId, int page = 1, int pageSize = 10);
        public Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(string userId, ReviewType type);
        public Task<decimal> GetPropertyAverageRatingAsync(string propertyId);
        public Task<int> GetPropertyReviewCountAsync(string propertyId);
        public Task<IEnumerable<Review>> GetReviewsByBookingAndTypeAsync(string bookingId, ReviewType type);
    }
}