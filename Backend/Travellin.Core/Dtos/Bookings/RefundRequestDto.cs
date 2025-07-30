namespace Travellin.Core.Dtos.Bookings
{
    public class RefundRequestDto
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; } = "requested_by_customer";
    }
} 