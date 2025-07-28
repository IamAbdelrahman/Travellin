using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.IdentityGovernance.AccessReviews.Definitions.FilterByCurrentUserWithOn;
using Microsoft.Graph.Models;
using System.Security.Claims;
using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Dtos.Notifications;
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
        private IIdentityFactory IdentityFactory { get; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        private string GetCurrentUserId() =>
         User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        public BookingsController(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, IIdentityFactory identityFactory, INotificationService notificationService)
        {
            ServiceFactory = serviceFactory;
            IdentityFactory = identityFactory;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        [Authorize(Roles = "Guest")]
        [HttpPost("Reserve")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingDto createBookingDto)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            var createBook = await ServiceFactory.BookingManagementService.CreateBookingAsync(userId, createBookingDto);

            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(createBook.PropertyId);
            if (property != null)
            {
                await _notificationService.NotifyBookingRequestAsync(property.OwnerId, new BookingRequestNotificationDto
                {
                    BookingId = createBook.Id,
                    GuestName = User.Identity.Name,
                    PropertyTitle = property.Title,
                    CheckIn = createBook.CheckIn,
                    CheckOut = createBook.CheckOut,
                    TotalAmount = createBook.TotalFees,
                    GuestCount = createBook.BookingGuests.Sum(g => g.GuestCount)
                });
            }

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

        [Authorize(Roles = "Host")]
        [HttpPost("{bookingId}/accept")]
        public async Task<IActionResult> AcceptBooking(string bookingId)
        {
            await ServiceFactory.BookingManagementService.AcceptBookingAsync(bookingId);
            
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking != null)
            {
                await _notificationService.NotifyBookingResponseAsync(booking.UserId, new BookingResponseNotificationDto
                {
                    BookingId = booking.Id,
                    HostName = User.Identity.Name,
                    PropertyTitle = booking.Property.Title,
                    Status = "confirmed"
                });

                await _notificationService.NotifyAdminForPaymentHold(new PaymentHoldNotificationDto
                {
                    BookingId = booking.Id,
                    Amount = booking.TotalFees
                });
            }

            return Ok(new { message = "Booking accepted." });
        }

        [Authorize(Roles = "Host")]
        [HttpPost("{bookingId}/decline")]
        public async Task<IActionResult> DeclineBooking(string bookingId)
        {
            await ServiceFactory.BookingManagementService.DeclineBookingAsync(bookingId);

            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking != null)
            {
                await _notificationService.NotifyBookingResponseAsync(booking.UserId, new BookingResponseNotificationDto
                {
                    BookingId = booking.Id,
                    HostName = User.Identity.Name,
                    PropertyTitle = booking.Property.Title,
                    Status = "cancelled"
                });
            }

            return Ok(new { message = "Booking declined." });
        }
    }
}
