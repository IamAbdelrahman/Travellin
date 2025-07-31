using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Dtos.Reviews;

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
    }
}