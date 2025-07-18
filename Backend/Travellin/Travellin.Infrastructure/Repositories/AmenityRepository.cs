using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly AirbnbDbContext db;

        public AmenityRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public List<Amenity> GetAll()
        {
            return db.Amenities.ToList();
        }

        //GetByID
        public Amenity GetByID(int id)
        {
            return db.Amenities.Find(id);
        }

        //Add
        public void Add(Amenity entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            Amenity amenity = GetByID(id);
            db.Amenities.Remove(amenity);
        }

        //Update
        public void Update(Amenity entity)
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
