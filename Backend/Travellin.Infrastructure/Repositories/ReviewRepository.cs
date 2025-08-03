using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Reviews;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Enums;
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
                .Where(x => x.Booking.PropertyId == propertyId && x.Type == ReviewType.Guest && x.Status == ReviewStatus.Published)
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
            var review = await _dbContext.Reviews
                .Include(x => x.Booking)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.UserProfile)
                .ThenInclude(x => x.Photo)
                .FirstOrDefaultAsync(x => x.Id == reviewId);

            return review?.ToDto();
        }

        // Enhanced review system methods
        public async Task<ReviewPeriodDto?> GetReviewPeriodAsync(string bookingId, string userId)
        {
            var booking = await _dbContext.Bookings
                .Include(x => x.Property)
                .FirstOrDefaultAsync(x => x.Id == bookingId);

            if (booking == null) return null;

            var checkOutDate = booking.CheckOut;
            var reviewPeriodStart = checkOutDate.AddDays(1);
            var reviewPeriodEnd = checkOutDate.AddDays(14);
            var now = DateTime.UtcNow;

            var hasGuestReview = await _dbContext.Reviews
                .AnyAsync(r => r.BookingId == bookingId && r.Type == ReviewType.Guest);

            var hasHostReview = await _dbContext.Reviews
                .AnyAsync(r => r.BookingId == bookingId && r.Type == ReviewType.Host);

            var canReviewAsGuest = booking.UserId == userId && 
                                  booking.Status == BookingStatus.Completed &&
                                  !hasGuestReview &&
                                  now >= reviewPeriodStart && 
                                  now <= reviewPeriodEnd;

            var canReviewAsHost = booking.Property.OwnerId == userId && 
                                 booking.Status == BookingStatus.Completed &&
                                 !hasHostReview &&
                                 now >= reviewPeriodStart && 
                                 now <= reviewPeriodEnd;

            var daysRemaining = Math.Max(0, (int)(reviewPeriodEnd - now).TotalDays);

            return new ReviewPeriodDto
            {
                BookingId = bookingId,
                CheckOutDate = checkOutDate,
                ReviewPeriodStart = reviewPeriodStart,
                ReviewPeriodEnd = reviewPeriodEnd,
                CanReviewAsGuest = canReviewAsGuest,
                CanReviewAsHost = canReviewAsHost,
                HasGuestReview = hasGuestReview,
                HasHostReview = hasHostReview,
                DaysRemaining = daysRemaining
            };
        }

        public async Task<bool> CanReviewAsync(string bookingId, string userId, ReviewType type)
        {
            var period = await GetReviewPeriodAsync(bookingId, userId);
            if (period == null) return false;
            
            if (type == ReviewType.Guest)
                return period.CanReviewAsGuest;
            
            if (type == ReviewType.Host)
                return period.CanReviewAsHost;
            
            return false;
        }

        public async Task<IEnumerable<ReviewDto>> GetPropertyReviewsAsync(string propertyId, int page = 1, int pageSize = 10)
        {
            var reviews = await _dbContext.Reviews
                .Include(x => x.Booking)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.UserProfile)
                .ThenInclude(x => x.Photo)
                .Where(x => x.Booking.PropertyId == propertyId && 
                           x.Type == ReviewType.Guest && 
                           x.Status == ReviewStatus.Published)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => x.ToDto())
                .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(string userId, ReviewType type)
        {
            var reviews = await _dbContext.Reviews
                .Include(x => x.Booking)
                .ThenInclude(x => x.Property)
                .Where(x => (type == ReviewType.Guest && x.Booking.UserId == userId) ||
                           (type == ReviewType.Host && x.Booking.Property.OwnerId == userId) &&
                           x.Type == type &&
                           x.Status == ReviewStatus.Published)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.ToDto())
                .ToListAsync();

            return reviews;
        }

        public async Task<decimal> GetPropertyAverageRatingAsync(string propertyId)
        {
            var averageRating = await _dbContext.Reviews
                .Where(x => x.Booking.PropertyId == propertyId && 
                           x.Type == ReviewType.Guest && 
                           x.Status == ReviewStatus.Published)
                .Select(x => (x.Cleanliness + x.Accuracy + x.CheckIn + x.Communication + x.Location + x.Value) / 6.0m)
                .DefaultIfEmpty(0)
                .AverageAsync();

            return Math.Round(averageRating, 1);
        }

        public async Task<int> GetPropertyReviewCountAsync(string propertyId)
        {
            return await _dbContext.Reviews
                .CountAsync(x => x.Booking.PropertyId == propertyId && 
                                x.Type == ReviewType.Guest && 
                                x.Status == ReviewStatus.Published);
        }

        public async Task<IEnumerable<Review>> GetReviewsByBookingAndTypeAsync(string bookingId, ReviewType type)
        {
            return await _dbContext.Reviews
                .Where(x => x.BookingId == bookingId && x.Type == type)
                .ToListAsync();
        }
    }
}