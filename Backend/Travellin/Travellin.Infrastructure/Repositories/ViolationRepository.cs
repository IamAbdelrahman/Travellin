using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class ViolationRepository : IViolationRepository
    {
        private readonly AirbnbDbContext db;

        public ViolationRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<Violation>> GetAllAsync()
        {
            return await db.Violations.ToListAsync();
        }

        //GetByID
        public async Task<Violation> GetByIDAsync(int id)
        {
            return await db.Violations.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Violation entity)
        {
            await db.Violations.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var violation = db.Violations.Find(id);
            if (violation != null)
            {
                db.Violations.Remove(violation);
            }
        }

        //Update
        public void Update(Violation entity)
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
