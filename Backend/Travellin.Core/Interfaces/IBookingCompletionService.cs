using System.Threading.Tasks;
using Travellin.Core.Dtos.Notifications;

namespace Travellin.Core.Interfaces
{
    public interface IBookingCompletionService
    {
        Task CompleteExpiredBookingsAsync();
        Task NotifyReviewPeriodStartAsync(string bookingId);
        Task NotifyReviewPeriodEndAsync(string bookingId);
        Task<bool> MarkBookingAsCompletedAsync(string bookingId);
    }
} 