using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AirbnbDbContext db;

        public LocationRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public List<Location> GetAll()
        {
            return db.Locations.ToList();
        }

        //GetByID
        public Location GetByID(int id)
        {
            return db.Locations.Find(id);
        }

        //Add
        public void Add(Location entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            Location location = GetByID(id);
            db.Locations.Remove(location);
        }

        //Update
        public void Update(Location entity)
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
