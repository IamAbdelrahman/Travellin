namespace Travellin.Core.Interfaces
{
    public interface IServiceFactory
    {
        public IAuthTokenService AuthTokenService { get; }
        public IFileUploadManagementService FileUploadManagementService { get; }
        public IBookingManagementService BookingManagementService { get; }
        public ICheckoutManagementService CheckoutManagementService { get; }
    }
}