using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AirbnbDbContext db;

        public PaymentRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public List<Payment> GetAll()
        {
            return db.Payments.ToList();
        }

        //GetByID
        public Payment GetByID(int id)
        {
            return db.Payments.Find(id);
        }

        //Add
        public void Add(Payment entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            Payment payment = GetByID(id);
            db.Payments.Remove(payment);
        }

        //Update
        public void Update(Payment entity)
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
