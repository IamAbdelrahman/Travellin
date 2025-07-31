using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class BookingRepository : GenericRepository<Booking, string>, IBookingRepository
    {
        public BookingRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<PaginatedResult<BookingDto>> GetByUserIdAsync(string userId, GetAllBookingsQueryParamsDto queryDto)
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
               .Where(x => x.UserId == userId)
               .AsQueryable();


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

