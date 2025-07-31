using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking, string>
    {
        //For userid get all bookings
        public Task<PaginatedResult<BookingDto>> GetByUserIdAsync(string userId, GetAllBookingsQueryParamsDto queryDto);
        //For booking id get booking details
        public Task<BookingDto> GetBookingDetailsAsync(string bookingId);
        //Get all bookings 
        public Task<PaginatedResult<BookingDto>> GetAllAsync(GetAllBookingsQueryParamsDto queryDto);

    }
}
