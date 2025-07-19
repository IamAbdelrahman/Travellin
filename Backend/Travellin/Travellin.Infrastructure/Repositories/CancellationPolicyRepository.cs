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
        public List<CancellationPolicy> GetAll()
        {
            return db.CancellationPolicies.ToList();
        }

        //GetByID
        public CancellationPolicy GetByID(int id)
        {
            return db.CancellationPolicies.Find(id);
        }

        //Add
        public void Add(CancellationPolicy entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            CancellationPolicy cancellationPolicy = GetByID(id);
            db.CancellationPolicies.Remove(cancellationPolicy);
        }

        //Update
        public void Update(CancellationPolicy entity)
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
