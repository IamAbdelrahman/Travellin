using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.IdentityGovernance.AccessReviews.Definitions.FilterByCurrentUserWithOn;
using System.Security.Claims;
using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Shared;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Travellin.Api.Controllers
{
    [Route("api/[controller]")]
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
            return Ok(new
            {
                Message = "Booking created successfully.",
                BookingId = createBook.Id,
                Status = createBook.Status,
                createBook.CheckIn,
                createBook.CheckOut,
                createBook.TotalFees
            });

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
    }
}
