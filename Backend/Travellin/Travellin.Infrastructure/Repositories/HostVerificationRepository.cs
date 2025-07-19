using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class HostVerificationRepository : IHostVerificationRepository
    {
        private readonly AirbnbDbContext db;

        public HostVerificationRepository(AirbnbDbContext db)
        {
            this.db = db;
        }


        //GetALL
        public async Task<List<HostVerification>> GetAllAsync()
        {
            return await db.HostVerifications.ToListAsync();
        }

        //GetByID
        public async Task<HostVerification> GetByIDAsync(int id)
        {
            return await db.HostVerifications.FindAsync(id);
        }

        //Add
        public async Task AddAsync(HostVerification entity)
        {
            await db.HostVerifications.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var hostVerification = db.HostVerifications.Find(id);
            if (hostVerification != null)
            {
                db.HostVerifications.Remove(hostVerification);
            }
        }


        //Update
        public void Update(HostVerification entity)
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
