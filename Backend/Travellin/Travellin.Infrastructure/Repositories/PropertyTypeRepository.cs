using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly AirbnbDbContext db;

        public PropertyTypeRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<PropertyType>> GetAllAsync()
        {
            return await db.PropertyTypes.ToListAsync();
        }

        //GetByID
        public async Task<PropertyType> GetByIDAsync(int id)
        {
            return await db.PropertyTypes.FindAsync(id);
        }

        //Add
        public async Task AddAsync(PropertyType entity)
        {
            await db.PropertyTypes.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var propertyType = db.PropertyTypes.Find(id);
            if (propertyType != null)
            {
                db.PropertyTypes.Remove(propertyType);
            }
        }

        //Update
        public void Update(PropertyType entity)
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
