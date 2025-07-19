using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Amenity>> GetAllAsync()
        {
            return await db.Amenities.ToListAsync();
        }

        //GetByID
        public async Task<Amenity> GetByIDAsync(int id)
        {
            return await db.Amenities.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Amenity entity)
        {
            await db.Amenities.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var amenity = db.Amenities.Find(id);
            if (amenity != null)
            {
                db.Amenities.Remove(amenity);
            }
        }


        //Update
        public void Update(Amenity entity)
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
