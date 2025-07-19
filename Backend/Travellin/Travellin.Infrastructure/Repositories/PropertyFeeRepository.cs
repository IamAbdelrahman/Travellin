using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class PropertyFeeRepository : IPropertyFeeRepository
    {
        private readonly AirbnbDbContext db;

        public PropertyFeeRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<PropertyFee>> GetAllAsync()
        {
            return await db.PropertyFees.ToListAsync();
        }

        //GetByID
        public async Task<PropertyFee> GetByIDAsync(int id)
        {
            return await db.PropertyFees.FindAsync(id);
        }

        //Add
        public async Task AddAsync(PropertyFee entity)
        {
            await db.PropertyFees.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var propertyFee = db.PropertyFees.Find(id);
            if (propertyFee != null)
            {
                db.PropertyFees.Remove(propertyFee);
            }
        }

        //Update
        public void Update(PropertyFee entity)
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
