using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.IdentityGovernance.AccessReviews.Definitions.FilterByCurrentUserWithOn;
using Microsoft.Graph.Models;
using System.Security.Claims;
using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Shared;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Travellin.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private IServiceFactory ServiceFactory { get; }
        private  IIdentityFactory IdentityFactory { get; }
        private readonly IUnitOfWork _unitOfWork;

        private string GetCurrentUserId() =>
         User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        public BookingsController(IUnitOfWork unitOfWork, IServiceFactory serviceFactory,IIdentityFactory identityFactory)
        {
            ServiceFactory = serviceFactory;
            IdentityFactory = identityFactory;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles ="Guest")]
        [HttpPost("Reserve")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingDto createBookingDto)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            var createBook = await ServiceFactory.BookingManagementService.CreateBookingAsync(userId, createBookingDto);

            var bookingDto = await _unitOfWork.BookingRepository.GetBookingDetailsAsync(createBook.Id);
            return new ObjectResult(bookingDto) { StatusCode = 201 };

        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBookingAsync(string id)
        {
            var userId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            if (userId == null)
                return Unauthorized();

            await ServiceFactory.BookingManagementService.CancelBookingAsync(id, userId, isAdmin);

            return Ok(new { Message = "Booking cancelled and availability restored." });
        }

        [Authorize]
        [HttpGet("HistoryBooking")]
        public async Task<ActionResult<PaginatedResult<BookingDto>>> GetMyBookings([FromQuery] GetAllBookingsQueryParamsDto queryDto)
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();
            var result = await _unitOfWork.BookingRepository.GetByUserIdAsync(userId, queryDto);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BookingDto>> GetBookingDetails(string id)
        {
            var booking = await _unitOfWork.BookingRepository.GetBookingDetailsAsync(id);
            if (booking is null)
                return NotFound($"Booking with ID {id} not found.");

            return Ok(booking);
        }

        [Authorize(Roles = "Host")]
        [HttpPost("{bookingId}/accept")]
        public async Task<IActionResult> AcceptBooking(string bookingId)
        {
            await ServiceFactory.BookingManagementService.AcceptBookingAsync(bookingId);
            return Ok(new { message = "Booking accepted." });
        }

        [Authorize(Roles = "Host")]
        [HttpPost("{bookingId}/decline")]
        public async Task<IActionResult> DeclineBooking(string bookingId)
        {
            await ServiceFactory.BookingManagementService.DeclineBookingAsync(bookingId);
            return Ok(new { message = "Booking declined." });
        }
    }
}
