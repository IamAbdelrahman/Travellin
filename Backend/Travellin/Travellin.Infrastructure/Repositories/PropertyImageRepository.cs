using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly AirbnbDbContext db;

        public PropertyImageRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<PropertyImage>> GetAllAsync()
        {
            return await db.PropertyImages.ToListAsync();
        }

        //GetByID
        public async Task<PropertyImage> GetByIDAsync(int id)
        {
            return await db.PropertyImages.FindAsync(id);
        }

        //Add
        public async Task AddAsync(PropertyImage entity)
        {
            await db.PropertyImages.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var propertyImage = db.PropertyImages.Find(id);
            if (propertyImage != null)
            {
                db.PropertyImages.Remove(propertyImage);
            }
        }

        //Update
        public void Update(PropertyImage entity)
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
