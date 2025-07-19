using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AirbnbDbContext db;

        public BookingRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<Booking>> GetAllAsync()
        {
           return await db.Bookings.ToListAsync();
        }

        //GetByID
        public async Task<Booking> GetByIDAsync(int id)
        {
            return await db.Bookings.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Booking entity)
        {
            await db.Bookings.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var booking = db.Bookings.Find(id);  
            if (booking != null)
            {
                db.Bookings.Remove(booking);
            }
        }


        //Update
        public void Update(Booking entity)
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
