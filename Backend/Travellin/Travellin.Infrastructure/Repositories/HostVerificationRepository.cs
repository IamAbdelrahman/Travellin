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
        public List<HostVerification> GetAll()
        {
            return db.HostVerifications.ToList();
        }

        //GetByID
        public HostVerification GetByID(int id)
        {
            return db.HostVerifications.Find(id);
        }

        //Add
        public void Add(HostVerification entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            HostVerification hostVerification = GetByID(id);
            db.HostVerifications.Remove(hostVerification);
        }

        //Update
        public void Update(HostVerification entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        //Save
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
