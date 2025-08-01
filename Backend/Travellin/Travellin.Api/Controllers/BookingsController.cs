using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.IdentityGovernance.AccessReviews.Definitions.FilterByCurrentUserWithOn;
using Microsoft.Graph.Models;
using System.Security.Claims;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Shared;
using Travellin.Travellin.Core.Shared;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Travellin.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private IServiceFactory ServiceFactory { get; }
        private  IIdentityFactory IdentityFactory { get; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICancellationService _cancellationService;
        private readonly IBookingManagementService _bookingManagementService;

        private string GetCurrentUserId() =>
         User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        public BookingsController(
            IUnitOfWork unitOfWork, 
            IServiceFactory serviceFactory,
            IIdentityFactory identityFactory,
            ICancellationService cancellationService,
            IBookingManagementService bookingManagementService)
        {
            ServiceFactory = serviceFactory;
            IdentityFactory = identityFactory;
            _unitOfWork = unitOfWork;
            _cancellationService = cancellationService;
            _bookingManagementService = bookingManagementService;
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


        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookings([FromQuery] GetAllBookingsQueryParamsDto queryDto)
        {
            var result = await _unitOfWork.BookingRepository.GetAllAsync(queryDto);
            return Ok(result);
        }

        // New enhanced cancellation endpoint
        [Authorize]
        [HttpPost("{id}/cancel-enhanced")]
        public async Task<IActionResult> CancelBookingEnhancedAsync(string id, [FromBody] CancellationRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            request.BookingId = id;
            request.CancelledByUserId = userId;
            request.IsHostCancellation = User.IsInRole("Host");

            var result = await _cancellationService.CancelBookingAsync(request);

            if (result.IsSuccessful)
            {
                return Ok(new { 
                    Message = result.Message,
                    RefundAmount = result.RefundAmount,
                    RefundId = result.RefundId
                });
            }
            else
            {
                return BadRequest(new { Message = result.Message });
            }
        }

        // New endpoint to check if booking can be cancelled
        [Authorize]
        [HttpGet("{id}/can-cancel")]
        public async Task<IActionResult> CanCancelBookingAsync(string id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            var isHost = User.IsInRole("Host");
            var canCancel = await _cancellationService.CanCancelBookingAsync(id, userId, isHost);
            var refundAmount = await _cancellationService.CalculateRefundAmountAsync(id, DateTime.UtcNow);
            var isWithinWindow = await _cancellationService.IsWithinCancellationWindowAsync(id, DateTime.UtcNow);

            return Ok(new { 
                CanCancel = canCancel,
                RefundAmount = refundAmount,
                IsWithinCancellationWindow = isWithinWindow
            });
        }

        // New endpoint for partial refunds
        [Authorize]
        [HttpPost("{id}/refund")]
        public async Task<IActionResult> ProcessRefundAsync(string id, [FromBody] RefundRequestDto request)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            var result = await _cancellationService.ProcessRefundAsync(id, request.Amount);

            if (result.IsSuccessful)
            {
                return Ok(new { 
                    Message = result.Message,
                    RefundAmount = result.RefundAmount,
                    RefundId = result.RefundId
                });
            }
            else
            {
                return BadRequest(new { Message = result.Message });
            }

        }

        [Authorize]
        [HttpGet("HistoryBookingOfUser")]
        public async Task<ActionResult<PaginatedResult<BookingDto>>> GetMyBookings([FromQuery] GetAllBookingsQueryParamsDto queryDto)
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();
            var result = await _unitOfWork.BookingRepository.GetByUserIdAsync(userId, queryDto);
            return Ok(result);
        }

        [HttpGet("{id}Get")]


        // New endpoint for hosts to see their property bookings
        [Authorize(Roles = "Host")]
        [HttpGet("host/bookings")]
        public async Task<ActionResult<PaginatedResult<BookingDto>>> GetHostBookings([FromQuery] GetAllBookingsQueryParamsDto queryDto)
        {
            var hostId = GetCurrentUserId();
            if (string.IsNullOrWhiteSpace(hostId))
                return Unauthorized();
            
            var result = await _unitOfWork.BookingRepository.GetByHostIdAsync(hostId, queryDto);
            return Ok(result);
        }

        // New endpoint for hosts to see pending bookings only
        [Authorize(Roles = "Host")]
        [HttpGet("host/pending-bookings")]
        public async Task<ActionResult<PaginatedResult<BookingDto>>> GetHostPendingBookings([FromQuery] GetAllBookingsQueryParamsDto queryDto)
        {
            try
            {
                var hostId = GetCurrentUserId();
                if (string.IsNullOrWhiteSpace(hostId))
                    return Unauthorized();
                
                var result = await _unitOfWork.BookingRepository.GetPendingBookingsForHostAsync(hostId, queryDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error in GetHostPendingBookings: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while retrieving pending bookings", error = ex.Message });
            }
        }

        // New endpoint for hosts to see bookings for a specific property
        [Authorize(Roles = "Host")]
        [HttpGet("host/property/{propertyId}/bookings")]
        public async Task<ActionResult<PaginatedResult<BookingDto>>> GetPropertyBookings(string propertyId, [FromQuery] GetAllBookingsQueryParamsDto queryDto)
        {
            var hostId = GetCurrentUserId();
            if (string.IsNullOrWhiteSpace(hostId))
                return Unauthorized();
            
            // TODO: Validate that the property belongs to this host
            var result = await _unitOfWork.BookingRepository.GetByPropertyIdAsync(propertyId, queryDto);
            return Ok(result);
        }

        // New endpoint for admins to see all bookings
        [Authorize(Roles = "Admin")]
        [HttpGet("admin/all-bookings")]
        public async Task<ActionResult<PaginatedResult<BookingDto>>> GetAllBookingsForAdmin([FromQuery] GetAllBookingsQueryParamsDto queryDto)
        {
            var result = await _unitOfWork.BookingRepository.GetAllBookingsForAdminAsync(queryDto);
            return Ok(result);
        }

        // New endpoint for admins to see pending bookings only
        [Authorize(Roles = "Admin")]
        [HttpGet("admin/pending-bookings")]
        public async Task<ActionResult<PaginatedResult<BookingDto>>> GetAdminPendingBookings([FromQuery] GetAllBookingsQueryParamsDto queryDto)
        {
            var result = await _unitOfWork.BookingRepository.GetPendingBookingsForAdminAsync(queryDto);
            return Ok(result);
        }

        // New endpoint to get pending bookings count for hosts
        [Authorize(Roles = "Host")]
        [HttpGet("host/pending-count")]
        public async Task<ActionResult<int>> GetHostPendingBookingsCount()
        {
            try
            {
                var hostId = GetCurrentUserId();
                if (string.IsNullOrWhiteSpace(hostId))
                    return Unauthorized();
                
                var count = await _unitOfWork.BookingRepository.GetPendingBookingsCountForHostAsync(hostId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error in GetHostPendingBookingsCount: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while retrieving pending count", error = ex.Message });
            }
        }

        // New endpoint to get pending bookings count for admins
        [Authorize(Roles = "Admin")]
        [HttpGet("admin/pending-count")]
        public async Task<ActionResult<int>> GetAdminPendingBookingsCount()
        {
            var count = await _unitOfWork.BookingRepository.GetPendingBookingsCountForAdminAsync();
            return Ok(count);
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
            await _bookingManagementService.AcceptBookingAsync(bookingId);
            return Ok(new { message = "Booking accepted." });
        }

        [Authorize(Roles = "Host")]
        [HttpPost("{bookingId}/decline")]
        public async Task<IActionResult> DeclineBooking(string bookingId)
        {
            await _bookingManagementService.DeclineBookingAsync(bookingId);
            return Ok(new { message = "Booking declined." });
        }

        // Debug endpoint to check database state
        [Authorize(Roles = "Host")]
        [HttpGet("host/debug")]
        public async Task<ActionResult<object>> DebugHostBookings()
        {
            try
            {
                var hostId = GetCurrentUserId();
                if (string.IsNullOrWhiteSpace(hostId))
                    return Unauthorized();

                // Get all bookings for this host
                var allBookingsResult = await _unitOfWork.BookingRepository.GetAllAsync(new GetAllQueryDto { Page = 1, PageSize = 1000 }, q => q.OrderBy(x => x.Id));
                var allBookings = allBookingsResult.Items.ToList();
                var hostBookings = allBookings.Where(b => b.Property != null && b.Property.OwnerId == hostId).ToList();
                var pendingBookings = hostBookings.Where(b => b.Status == BookingStatus.Pending).ToList();
                
                // Get properties owned by this host
                var hostPropertiesResult = await _unitOfWork.PropertyRepository.GetAllAsync(new GetAllQueryDto { Page = 1, PageSize = 1000 }, q => q.OrderBy(x => x.Id));
                var hostProperties = hostPropertiesResult.Items.ToList();
                var ownedProperties = hostProperties.Where(p => p.OwnerId == hostId).ToList();

                var debugInfo = new
                {
                    hostId = hostId,
                    totalBookings = allBookings.Count,
                    hostBookings = hostBookings.Count,
                    pendingBookings = pendingBookings.Count,
                    ownedProperties = ownedProperties.Count,
                    bookingsWithNullProperty = allBookings.Count(b => b.Property == null),
                    bookingsWithNullOwnerId = allBookings.Count(b => b.Property != null && b.Property.OwnerId == null),
                    pendingBookingsDetails = pendingBookings.Select(b => new
                    {
                        id = b.Id,
                        propertyId = b.PropertyId,
                        propertyOwnerId = b.Property?.OwnerId,
                        status = b.Status.ToString(),
                        checkIn = b.CheckIn,
                        checkOut = b.CheckOut
                    }).ToList()
                };

                return Ok(debugInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Debug error", error = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }
}
