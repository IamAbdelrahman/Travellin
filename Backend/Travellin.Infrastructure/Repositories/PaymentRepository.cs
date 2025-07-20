using Microsoft.EntityFrameworkCore;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class PaymentRepository : GenericRepository<Payment, string>, IPaymentRepository
    {
        public PaymentRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<Payment?> GetPaymentBySessionIdAsync(string sessionId)
        {
            return await _dbContext.Payments.Where(x => x.StripeSessionId == sessionId).FirstOrDefaultAsync();
        }
    }
}
