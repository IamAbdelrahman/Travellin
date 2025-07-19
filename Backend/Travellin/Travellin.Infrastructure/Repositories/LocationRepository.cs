using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Location>> GetAllAsync()
        {
            return await db.Locations.ToListAsync();
        }

        //GetByID
        public async Task<Location> GetByIDAsync(int id)
        {
            return await db.Locations.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Location entity)
        {
            await db.Locations.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var location = db.Locations.Find(id);
            if (location != null)
            {
                db.Locations.Remove(location);
            }
        }

        //Update
        public void Update(Location entity)
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
