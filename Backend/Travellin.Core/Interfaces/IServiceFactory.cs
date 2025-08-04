namespace Travellin.Core.Interfaces
{
    public interface IServiceFactory
    {
        public IAuthTokenService AuthTokenService { get; }
        public IFileUploadManagementService FileUploadManagementService { get; }
        public IBookingManagementService BookingManagementService { get; }
        public ICheckoutManagementService CheckoutManagementService { get; }
        IConversationService ConversationService { get; }
        IMessageService MessageService { get; }
        public IPropertyFilterExtractorService PropertyFilterExtractorService { get; }
        public INotificationService NotificationService { get; }
        //public IReviewService ReviewService { get; }
    }
}