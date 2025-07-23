using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.IdentityGovernance.AccessReviews.Definitions.FilterByCurrentUserWithOn;
using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Shared;

namespace Travellin.Travellin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        public IServiceFactory ServiceFactory { get; }
        public IIdentityFactory IdentityFactory { get; }

        public BookingsController(IServiceFactory serviceFactory,IIdentityFactory identityFactory)
        {
            ServiceFactory = serviceFactory;
            IdentityFactory = identityFactory;
        }

        [Authorize(Roles ="Guest")]
        [HttpPost("Reserve")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingDto createBookingDto)
        {
            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
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
            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            var isAdmin = User.IsInRole("Admin");

            if (userId == null)
                return Unauthorized();

            await ServiceFactory.BookingManagementService.CancelBookingAsync(id, userId, isAdmin);

            return Ok(new { Message = "Booking cancelled and availability restored." });
        }
    }
}
