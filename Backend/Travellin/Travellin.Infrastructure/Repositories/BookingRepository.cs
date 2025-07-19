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
        public List<Booking> GetAll()
        {
            return db.Bookings.ToList();
        }

        //GetByID
        public Booking GetByID(int id)
        {
            return db.Bookings.Find(id);
        }

        //Add
        public void Add(Booking entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            Booking booking = GetByID(id);
            db.Bookings.Remove(booking);
        }

        //Update
        public void Update(Booking entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        //Save
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
