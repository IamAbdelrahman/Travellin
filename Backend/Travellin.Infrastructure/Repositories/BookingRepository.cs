using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Infrastructure.Repositories
{
    class BookingRepository : GenericRepository<Booking, string>, IBookingRepository
    {
        public BookingRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<PaginatedResult<BookingDto>> GetByUserIdAsync(string userId, GetAllBookingsQueryParamsDto queryDto)
        {
            var query = _dbContext.Bookings
                .Include(x => x.Property)
                .Include(x => x.User)
                .Include(x => x.BookingGuests)
                .Where(x => x.UserId == userId);

            return await GetPaginatedBookingsAsync(query, queryDto);
        }

        // New method for hosts to get their property bookings
        public async Task<PaginatedResult<BookingDto>> GetByHostIdAsync(string hostId, GetAllBookingsQueryParamsDto queryDto)
        {
            var query = _dbContext.Bookings
                .Include(x => x.Property)
                .ThenInclude(x => x.Owner)
                .Include(x => x.User)
                .Include(x => x.BookingGuests)
                .Where(x => x.Property != null && 
                           x.Property.OwnerId != null && 
                           x.Property.OwnerId == hostId);

            return await GetPaginatedBookingsAsync(query, queryDto);
        }

        // New method for getting bookings by property
        public async Task<PaginatedResult<BookingDto>> GetByPropertyIdAsync(string propertyId, GetAllBookingsQueryParamsDto queryDto)
        {
            var query = _dbContext.Bookings
                .Include(x => x.Property)
                .ThenInclude(x => x.Owner)
                .Include(x => x.User)
                .Include(x => x.BookingGuests)
                .Where(x => x.PropertyId == propertyId);

            return await GetPaginatedBookingsAsync(query, queryDto);
        }

        // New method for admins to get all bookings
        public async Task<PaginatedResult<BookingDto>> GetAllBookingsForAdminAsync(GetAllBookingsQueryParamsDto queryDto)
        {
            var query = _dbContext.Bookings
                .Include(x => x.Property)
                .ThenInclude(x => x.Owner)
                .Include(x => x.User)
                .Include(x => x.BookingGuests);

            return await GetPaginatedBookingsAsync(query, queryDto);
        }

        // New method for hosts to get pending bookings
        public async Task<PaginatedResult<BookingDto>> GetPendingBookingsForHostAsync(string hostId, GetAllBookingsQueryParamsDto queryDto)
        {
            try
            {
                // First, let's check if there are any bookings with null properties
                var bookingsWithNullProperty = await _dbContext.Bookings
                    .Where(x => x.Property == null)
                    .CountAsync();
                
                if (bookingsWithNullProperty > 0)
                {
                    Console.WriteLine($"Warning: Found {bookingsWithNullProperty} bookings with null Property");
                }

                var query = _dbContext.Bookings
                    .Include(x => x.Property)
                    .ThenInclude(x => x.Owner)
                    .Include(x => x.User)
                    .Include(x => x.BookingGuests)
                    .Where(x => x.Property != null && 
                               x.Property.OwnerId != null && 
                               x.Property.OwnerId == hostId && 
                               x.Status == BookingStatus.Pending);

                return await GetPaginatedBookingsAsync(query, queryDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error in GetPendingBookingsForHostAsync: {ex.Message}");
                Console.WriteLine($"HostId: {hostId}");
                throw;
            }
        }

        // New method for admins to get pending bookings
        public async Task<PaginatedResult<BookingDto>> GetPendingBookingsForAdminAsync(GetAllBookingsQueryParamsDto queryDto)
        {
            var query = _dbContext.Bookings
                .Include(x => x.Property)
                .ThenInclude(x => x.Owner)
                .Include(x => x.User)
                .Include(x => x.BookingGuests)
                .Where(x => x.Status == BookingStatus.Pending);

            return await GetPaginatedBookingsAsync(query, queryDto);
        }

        // New method to get pending bookings count for hosts
        public async Task<int> GetPendingBookingsCountForHostAsync(string hostId)
        {
            try
            {
                return await _dbContext.Bookings
                    .Where(x => x.Property != null && 
                               x.Property.OwnerId != null && 
                               x.Property.OwnerId == hostId && 
                               x.Status == BookingStatus.Pending)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error in GetPendingBookingsCountForHostAsync: {ex.Message}");
                Console.WriteLine($"HostId: {hostId}");
                throw;
            }
        }

        // New method to get pending bookings count for admins
        public async Task<int> GetPendingBookingsCountForAdminAsync()
        {
            return await _dbContext.Bookings
                .Where(x => x.Status == BookingStatus.Pending)
                .CountAsync();
        }

        public async Task<BookingDto> GetBookingDetailsAsync(string bookingId)
        {
            var query = _dbContext.Bookings
               .Include(x => x.BookingGuests)
               .Include(x => x.Property)
               .ThenInclude(x => x.Bookings)
                .ThenInclude(x => x.Review)
                .Include(x => x.Property)
                .ThenInclude(x => x.PropertyPhotos)
                .ThenInclude(x => x.FileUpload)
                .Include(x => x.Property)
                .ThenInclude(x => x.Owner)
                .Include(x => x.Property)
                .ThenInclude(x => x.Location)
                .Include(x => x.Property)
                .ThenInclude(x => x.PropertyType)
               .Where(x => x.Id == bookingId)
               .AsQueryable();

            var booking = await query
                                .Select(x => x.ToDto())
                                .FirstOrDefaultAsync();
            return booking;
        }

        // Helper method for paginated booking queries
        private async Task<PaginatedResult<BookingDto>> GetPaginatedBookingsAsync(IQueryable<Booking> query, GetAllBookingsQueryParamsDto queryDto)
        {
            // Apply filters
            if (queryDto.Status is not null)
                query = query.Where(x => x.Status == queryDto.GetStatusAsEnum());

            if (queryDto.CheckIn is not null)
                query = query.Where(x => x.CheckIn == queryDto.CheckIn);

            if (queryDto.CheckOut is not null)
                query = query.Where(x => x.CheckOut == queryDto.CheckOut);

            var total = await query.CountAsync();

            var items = await query
                .Skip(queryDto.CalcSkippedItems())
                .Take(queryDto.PageSize)
                .Select(x => x.ToDto())
                .ToListAsync();

            return new PaginatedResult<BookingDto>
            {
                Items = items,
                MetaData = new PaginationMetaData
                {
                    Page = queryDto.Page,
                    PageSize = queryDto.PageSize,
                    Total = total
                }
            };
        }


        public async Task<BookingDto> GetBookingDetailsAsync(string bookingId)
        {
            var query = _dbContext.Bookings
               .Include(x => x.BookingGuests)
               .Include(x => x.Property)
               .ThenInclude(x => x.Bookings)
                .ThenInclude(x => x.Review)
                .Include(x => x.Property)
                .ThenInclude(x => x.PropertyPhotos)
                .ThenInclude(x => x.FileUpload)
                .Include(x => x.Property)
                .ThenInclude(x => x.Owner)
                .Include(x => x.Property)
                .ThenInclude(x => x.Location)
                .Include(x => x.Property)
                .ThenInclude(x => x.PropertyType)
               .Where(x => x.Id == bookingId)
               .AsQueryable();

            var booking = await query
                                .Select(x => x.ToDto())
                                .FirstOrDefaultAsync();
            return booking;
        }

        public async Task<PaginatedResult<BookingDto>> GetAllAsync(GetAllBookingsQueryParamsDto queryDto)
        {
            var query = _dbContext.Bookings
                       .AsNoTracking()
                       .Include(b => b.User) 
                       .Include(b => b.Property)
                       .Include(b => b.BookingGuests)
                         .ThenInclude(bg => bg.GuestType)
                       .AsQueryable();

            if (queryDto.Status is not null)
                query = query.Where(x => x.Status == queryDto.GetStatusAsEnum());

            if (queryDto.CheckIn is not null)
                query = query.Where(x => x.CheckIn == queryDto.CheckIn);

            if (queryDto.CheckOut is not null)
                query = query.Where(x => x.CheckOut == queryDto.CheckOut);

            var total = await query.CountAsync();

            var bookings = await query
                .Where(b => b.User != null) 
                .OrderByDescending(b => b.CreatedAt)
                .Skip(queryDto.CalcSkippedItems())
                .Take(queryDto.PageSize)
                .Select(x => x.ToDto())
                .ToListAsync();

            return new PaginatedResult<BookingDto>
            {
                Items = bookings,
                MetaData = new PaginationMetaData
                {
                    Page = queryDto.Page,
                    PageSize = queryDto.PageSize,
                    Total = total
                }
            };
        }
    }

}

