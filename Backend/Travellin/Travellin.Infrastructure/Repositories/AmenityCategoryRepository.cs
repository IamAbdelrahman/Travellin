using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class AmenityCategoryRepository : IAmenityCategoryRepository
    {
        private readonly AirbnbDbContext db;

        public AmenityCategoryRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<AmenityCategory>> GetAllAsync()
        {
            return await db.AmenityCategories.ToListAsync();
        }

        //GetByID
        public async Task<AmenityCategory> GetByIDAsync(int id)
        {
            return await db.AmenityCategories.FindAsync(id);
        }

        //Add
        public async Task AddAsync(AmenityCategory entity)
        {
            await db.AmenityCategories.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var amenityCategory = db.AmenityCategories.Find(id);
            if (amenityCategory != null)
            {
                db.AmenityCategories.Remove(amenityCategory);
            }
        }


        //Update
        public void Update(AmenityCategory entity)
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
