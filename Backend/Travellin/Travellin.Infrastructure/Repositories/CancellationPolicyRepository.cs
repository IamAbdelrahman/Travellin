using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class CancellationPolicyRepository : ICancellationPolicyRepository
    {
        private readonly AirbnbDbContext db;

        public CancellationPolicyRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<CancellationPolicy>> GetAllAsync()
        {
            return await db.CancellationPolicies.ToListAsync();
        }

        //GetByID
        public async Task<CancellationPolicy> GetByIDAsync(int id)
        {
            return await db.CancellationPolicies.FindAsync(id);
        }

        //Add
        public async Task AddAsync(CancellationPolicy entity)
        {
            await db.CancellationPolicies.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var cancel = db.CancellationPolicies.Find(id);
            if (cancel != null)
            {
                db.CancellationPolicies.Remove(cancel);
            }
        }


        //Update
        public void Update(CancellationPolicy entity)
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
