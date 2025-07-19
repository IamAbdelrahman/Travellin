using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class PropertyAvailabilityRepository : IPropertyAvailabilityRepository
    {
        private readonly AirbnbDbContext db;

        public PropertyAvailabilityRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<PropertyAvailability>> GetAllAsync()
        {
            return await db.PropertyAvailabilities.ToListAsync();
        }

        //GetByID
        public async Task<PropertyAvailability> GetByIDAsync(int id)
        {
            return await db.PropertyAvailabilities.FindAsync(id);
        }

        //Add
        public async Task AddAsync(PropertyAvailability entity)
        {
            await db.PropertyAvailabilities.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var propertyAvailability = db.PropertyAvailabilities.Find(id);
            if (propertyAvailability != null)
            {
                db.PropertyAvailabilities.Remove(propertyAvailability);
            }
        }

        //Update
        public void Update(PropertyAvailability entity)
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
