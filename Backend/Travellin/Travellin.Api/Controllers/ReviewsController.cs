using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Travellin.Core.Dtos.Reviews;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Services;

namespace Travellin.Travellin.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IUnitOfWork _unitOfWork;
        public ReviewsController(IReviewService reviewService, IUnitOfWork unitOfWork)
        {
            _reviewService = reviewService;
            _unitOfWork=unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews()
        {
            try
            {
                //var reviews = await _reviewService.GetAllAsync();
                var reviews = await _unitOfWork.ReviewRepository.GetAll();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReviewById(string id)
        {
            try
            {
                //var review = await _reviewService.GetByIdAsync(id);
                var review = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
                if (review == null)
                {
                    return NotFound();
                }
                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByBookingId(string bookingId)
        {
            try
            {
                var reviews = await _reviewService.GetByBookingIdAsync(bookingId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] CreateReviewDto createReviewDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdReview = await _reviewService.CreateAsync(createReviewDto);
                return CreatedAtAction(nameof(GetReviewById), new { id = createdReview.Id }, createdReview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(string id, [FromBody] ReviewDto reviewDto)
        {
            try
            {
                if (id != reviewDto.Id)
                {
                    return BadRequest("ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updated = await _reviewService.UpdateAsync(reviewDto);
                if (!updated)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(string id)
        {
            try
            {
                var deleted = await _reviewService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}