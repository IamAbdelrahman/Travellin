using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;

namespace Travellin.Core.Interfaces
{
    public interface IBookingManagementService
    {
        public Task<Booking> CreateBookingAsync(string userId, CreateBookingDto dto);
        public Task CancelBookingAsync(string bookingId, string userId, bool isAdmin);
    }
}
