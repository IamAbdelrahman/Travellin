using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Dtos.Reviews;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Core.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAllAsync();
        Task<ReviewDto> GetByIdAsync(string id);
        Task<IEnumerable<ReviewDto>> GetByBookingIdAsync(string bookingId);
        Task<ReviewDto> CreateAsync(CreateReviewDto createReviewDto);
        Task<bool> UpdateAsync(ReviewDto reviewDto);
        Task<bool> DeleteAsync(string id);
        
        // Enhanced review system methods
        Task<ReviewPeriodDto> GetReviewPeriodAsync(string bookingId, string userId);
        Task<bool> CanReviewAsync(string bookingId, string userId, ReviewType type);
        Task<IEnumerable<ReviewDto>> GetPropertyReviewsAsync(string propertyId, int page = 1, int pageSize = 10);
        Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(string userId, ReviewType type);
        Task<bool> PublishReviewAsync(string reviewId);
        Task<bool> HideReviewAsync(string reviewId);
        Task<decimal> GetPropertyAverageRatingAsync(string propertyId);
        Task<int> GetPropertyReviewCountAsync(string propertyId);
    }
}