using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class PropertyAmenityRepository : IPropertyAmenityRepository
    {
        private readonly AirbnbDbContext db;

        public PropertyAmenityRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<PropertyAmenity>> GetAllAsync()
        {
            return await db.PropertyAmenities.ToListAsync();
        }

        //GetByID
        public async Task<PropertyAmenity> GetByIDAsync(int id)
        {
            return await db.PropertyAmenities.FindAsync(id);
        }

        //Add
        public async Task AddAsync(PropertyAmenity entity)
        {
            await db.PropertyAmenities.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var propertyAmenity = db.PropertyAmenities.Find(id);
            if (propertyAmenity != null)
            {
                db.PropertyAmenities.Remove(propertyAmenity);
            }
        }

        //Update
        public void Update(PropertyAmenity entity)
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
