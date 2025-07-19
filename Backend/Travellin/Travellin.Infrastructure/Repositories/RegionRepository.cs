using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AirbnbDbContext db;

        public RegionRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<Region>> GetAllAsync()
        {
            return await db.Regions.ToListAsync();
        }

        //GetByID
        public async Task<Region> GetByIDAsync(int id)
        {
            return await db.Regions.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Region entity)
        {
            await db.Regions.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var region = db.Regions.Find(id);
            if (region != null)
            {
                db.Regions.Remove(region);
            }
        }

        //Update
        public void Update(Region entity)
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
