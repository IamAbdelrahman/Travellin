using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AirbnbDbContext db;

        public PropertyRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<Property>> GetAllAsync()
        {
            return await db.Properties.ToListAsync();
        }

        //GetByID
        public async Task<Property> GetByIDAsync(int id)
        {
            return await db.Properties.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Property entity)
        {
            await db.Properties.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var property = db.Properties.Find(id);
            if (property != null)
            {
                db.Properties.Remove(property);
            }
        }

        //Update
        public void Update(Property entity)
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
