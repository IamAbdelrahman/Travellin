using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking, string>
    {
        public Task<PaginatedResult<BookingDto>> GetByUserIdAsync(string userId, GetAllBookingsQueryParamsDto queryDto);
        public Task<BookingDto> GetBookingDetailsByIdAsync(string bookingId);
    }
}
