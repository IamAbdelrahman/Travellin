using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class BookingGuestRepository : IBookingGuestRepository
    {

        private readonly AirbnbDbContext db;

        public BookingGuestRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<BookingGuest>> GetAllAsync()
        {
            return await db.BookingGuests.ToListAsync();
        }

        //GetByID
        public async Task<BookingGuest> GetByIDAsync(int id)
        {
            return await db.BookingGuests.FindAsync(id);
        }

        //Add
        public async Task AddAsync(BookingGuest entity)
        {
            await db.BookingGuests.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var bookingGuest = db.BookingGuests.Find(id);
            if (bookingGuest != null)
            {
                db.BookingGuests.Remove(bookingGuest);
            }
        }


        //Update
        public void Update(BookingGuest entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        //Save
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
