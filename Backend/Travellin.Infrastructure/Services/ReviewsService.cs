using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Reviews;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Infrastructure.Data;
using Travellin.Travellin.Core.Enums;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Infrastructure.Services
{
    public class ReviewsService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TravellinDbContext _dbContext;

        public ReviewsService(IReviewRepository reviewRepository, IUnitOfWork unitOfWork, TravellinDbContext dbContext)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllAsync()
        {
            var reviews = await _dbContext.Reviews
                .Include(x => x.Booking)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.UserProfile)
                .ThenInclude(x => x.Photo)
                .Select(x => x.ToDto())
                .ToListAsync();

            return reviews;
        }

        public async Task<PaginatedResult<ReviewDto>> GetAllAsync(GetAllQueryDto queryDto,
            Func<IQueryable<Review>, IOrderedQueryable<Review>> orderBy)
        {
            var result = await _reviewRepository.GetAllAsync(queryDto, orderBy);
            return new PaginatedResult<ReviewDto>
            {
                Items = result.Items.Select(x => x.ToDto()),
                MetaData = result.MetaData
            };
        }

        public async Task<ReviewDto?> GetByIdAsync(string id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            return review?.ToDto();
        }

        public async Task<ReviewDto?> GetReviewDetailsAsync(string reviewId)
        {
            return await _reviewRepository.GetReviewDetails(reviewId);
        }

        public async Task<PaginatedResult<ReviewDto>> GetPropertyReviewsAsync(string propertyId, GetAllQueryDto dto)
        {
            return await _reviewRepository.GetPropertyReviews(propertyId, dto);
        }

        public async Task<ReviewDto> CreateAsync(CreateReviewDto createReviewDto)
        {
            // Validate that UserId is provided
            if (string.IsNullOrEmpty(createReviewDto.UserId))
            {
                throw new InvalidOperationException("User ID is required to submit a review");
            }

            // Validate review eligibility
            var canReview = await CanReviewAsync(createReviewDto.BookingId, createReviewDto.UserId, createReviewDto.Type);
            if (!canReview)
            {
                throw new InvalidOperationException("Cannot submit review at this time");
            }

            var review = new Review
            {
                BookingId = createReviewDto.BookingId,
                Comment = createReviewDto.Comment,
                Cleanliness = createReviewDto.Cleanliness,
                Accuracy = createReviewDto.Accuracy,
                CheckIn = createReviewDto.CheckIn,
                Communication = createReviewDto.Communication,
                Location = createReviewDto.Location,
                Value = createReviewDto.Value,
                Type = createReviewDto.Type,
                IsAnonymous = createReviewDto.IsAnonymous,
                Status = ReviewStatus.Submitted,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            return review.ToDto();
        }

        public async Task<bool> UpdateAsync(ReviewDto reviewDto)
        {
            var existingReview = await _dbContext.Reviews
                .FirstOrDefaultAsync(r => r.BookingId == reviewDto.BookingId && r.Type == reviewDto.Type);

            if (existingReview == null)
                return false;

            existingReview.Comment = reviewDto.Comment;
            existingReview.Cleanliness = reviewDto.Cleanliness;
            existingReview.Accuracy = reviewDto.Accuracy;
            existingReview.CheckIn = reviewDto.CheckIn;
            existingReview.Communication = reviewDto.Communication;
            existingReview.Location = reviewDto.Location;
            existingReview.Value = reviewDto.Value;
            existingReview.IsAnonymous = reviewDto.IsAnonymous;
            existingReview.UpdatedAt = DateTime.UtcNow;

            _dbContext.Reviews.Update(existingReview);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
                return false;

            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // Enhanced review system methods
        public async Task<ReviewPeriodDto> GetReviewPeriodAsync(string bookingId, string userId)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId, x => x.Property);
            if (booking == null)
                throw new NotFoundException("Booking not found");

            var checkOutDate = booking.CheckOut;
            var reviewPeriodStart = checkOutDate.AddDays(1); // Review period starts day after checkout
            var reviewPeriodEnd = checkOutDate.AddDays(14); // 14 days to submit review
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
            
            if (type == ReviewType.Guest)
                return period.CanReviewAsGuest;
            
            if (type == ReviewType.Host)
                return period.CanReviewAsHost;
            
            return false;
        }

        public async Task<IEnumerable<ReviewDto>> GetPropertyReviewsAsync(string propertyId, int page = 1, int pageSize = 10)
        {
            return await _reviewRepository.GetPropertyReviewsAsync(propertyId, page, pageSize);
        }

        public async Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(string userId, ReviewType type)
        {
            return await _reviewRepository.GetUserReviewsAsync(userId, type);
        }

        public async Task<bool> PublishReviewAsync(string reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null) return false;

            review.Status = ReviewStatus.Published;
            review.UpdatedAt = DateTime.UtcNow;
            
            _dbContext.Reviews.Update(review);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HideReviewAsync(string reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null) return false;

            review.Status = ReviewStatus.Hidden;
            review.UpdatedAt = DateTime.UtcNow;
            
            _dbContext.Reviews.Update(review);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetPropertyAverageRatingAsync(string propertyId)
        {
            return await _reviewRepository.GetPropertyAverageRatingAsync(propertyId);
        }

        public async Task<int> GetPropertyReviewCountAsync(string propertyId)
        {
            return await _reviewRepository.GetPropertyReviewCountAsync(propertyId);
        }

        public async Task<IEnumerable<ReviewDto>> GetByBookingIdAsync(string bookingId)
        {
            var reviews = await _dbContext.Reviews
                .Where(r => r.BookingId == bookingId)
                .ToListAsync();
            
            return reviews.Select(r => r.ToDto());
        }
    }
}