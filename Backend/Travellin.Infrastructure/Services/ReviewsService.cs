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
using Travellin.Travellin.Core.Shared;

namespace Travellin.Infrastructure.Services
{
    public class ReviewsService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly TravellinDbContext _dbContext;

        public ReviewsService(IReviewRepository reviewRepository, TravellinDbContext dbContext)
        {
            _reviewRepository = reviewRepository;
            _dbContext = dbContext;
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
        .FirstOrDefaultAsync(r => r.BookingId == reviewDto.BookingId);

            if (existingReview == null)
                return false;

            existingReview.Comment = reviewDto.Comment;
            existingReview.Cleanliness = reviewDto.Cleanliness;
            existingReview.Accuracy = reviewDto.Accuracy;
            existingReview.CheckIn = reviewDto.CheckIn;
            existingReview.Communication = reviewDto.Communication;
            existingReview.Location = reviewDto.Location;
            existingReview.Value = reviewDto.Value;
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

        public async Task<IEnumerable<ReviewDto>> GetByBookingIdAsync(string bookingId)
        {
            var result = await _reviewRepository.GetPropertyReviews(bookingId, new GetAllQueryDto { PageSize = int.MaxValue });
            return result.Items;
        }

        public Task<IEnumerable<ReviewDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}