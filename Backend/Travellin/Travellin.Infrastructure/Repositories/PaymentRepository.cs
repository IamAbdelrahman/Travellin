using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Payment>> GetAllAsync()
        {
            return await db.Payments.ToListAsync();
        }

        //GetByID
        public async Task<Payment> GetByIDAsync(int id)
        {
            return await db.Payments.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Payment entity)
        {
            await db.Payments.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var payment = db.Payments.Find(id);
            if (payment != null)
            {
                db.Payments.Remove(payment);
            }
        }

        //Update
        public void Update(Payment entity)
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
