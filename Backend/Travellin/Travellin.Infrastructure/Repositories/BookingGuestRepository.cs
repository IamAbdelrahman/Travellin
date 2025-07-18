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
        public List<BookingGuest> GetAll()
        {
            return db.BookingGuests.ToList();
        }

        //GetByID
        public BookingGuest GetByID(int id)
        {
            return db.BookingGuests.Find(id);
        }

        //Add
        public void Add(BookingGuest entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            BookingGuest bookingGuest = GetByID(id);
            db.BookingGuests.Remove(bookingGuest);
        }

        //Update
        public void Update(BookingGuest entity)
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
