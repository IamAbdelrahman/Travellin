using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking, string>
    {
        Task<BookingDto?> GetBookingDetailsAsync(string id);
        Task<PaginatedResult<BookingDto>> GetByUserIdAsync(string userId, GetAllBookingsQueryParamsDto queryDto);
        
        // New methods for host and admin management
        Task<PaginatedResult<BookingDto>> GetByHostIdAsync(string hostId, GetAllBookingsQueryParamsDto queryDto);
        Task<PaginatedResult<BookingDto>> GetByPropertyIdAsync(string propertyId, GetAllBookingsQueryParamsDto queryDto);
        Task<PaginatedResult<BookingDto>> GetAllBookingsForAdminAsync(GetAllBookingsQueryParamsDto queryDto);
        Task<PaginatedResult<BookingDto>> GetPendingBookingsForHostAsync(string hostId, GetAllBookingsQueryParamsDto queryDto);
        Task<PaginatedResult<BookingDto>> GetPendingBookingsForAdminAsync(GetAllBookingsQueryParamsDto queryDto);
        Task<int> GetPendingBookingsCountForHostAsync(string hostId);
        Task<int> GetPendingBookingsCountForAdminAsync();
    }
}
