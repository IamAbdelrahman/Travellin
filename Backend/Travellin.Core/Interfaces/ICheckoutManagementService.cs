namespace Travellin.Core.Interfaces
{
    public class PropertyInfo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? MainPhotoUrl { get; set; }
        public string LocationName { get; set; }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }

    public class BookingPeriod
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Nights => (CheckOutDate - CheckInDate).Days;
    }

    public class PricingDetails
    {
        public decimal PricePerNight { get; set; }
        public decimal TotalFees { get; set; }
        public decimal TotalAmount { get; set; }
    }


    public class CheckoutOptions
    {
        public string BookingId { get; set; }
        public UserInfo Guest { get; set; }
        public PropertyInfo Property { get; set; }
        public BookingPeriod BookingPeriod { get; set; }
        public PricingDetails Pricing { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = new();
        public decimal TotalAmount => Pricing.TotalAmount * BookingPeriod.Nights;
    }

    public class CreateCheckoutResult
    {
        public string SessionId { get; set; }
        public string SessionUrl { get; set; }
        public DateTime ExpiresAt { get; set; }
    }


    public interface ICheckoutManagementService
    {
        Task<CreateCheckoutResult> CreateCheckoutSessionAsync(CheckoutOptions options);
        Task HandlePaymentWebhookAsync(string json, string signature);
    }
}
