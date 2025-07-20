using Travellin.Core.Entities;

namespace Travellin.Core.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment, string>
    {
        public Task<Payment?> GetPaymentBySessionIdAsync(string sessionId);
    }
}
