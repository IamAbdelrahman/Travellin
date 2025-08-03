using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OpenAI.Chat;
using Stripe;
using Travellin.Core.Interfaces;
using Travellin.Core.Services;
using Travellin.Infrastructure.Services;

namespace Travellin.Infrastructure.Shared
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _provider;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private IAuthTokenService _authTokenService;
        private IFileUploadManagementService _fileUploadManagementService;
        private IBookingManagementService _bookingManagementService;
        private ICheckoutManagementService _checkoutManagementService;
        private IConversationService? _conversationService;
        private IMessageService? _messageService;
        private IPropertyFilterExtractorService _propertyFilterExtractorService;
        //private IReviewService _reviewService;
        private IStripeTransferService _stripeTransferService; 
        private readonly ILogger<StripeCheckoutService> _logger;
        private readonly ILogger<StripeTransferService> _loggerTransfer;

        public ServiceFactory(IServiceProvider provider, IConfiguration config)
        {
            _provider = provider;
            _config = config;
            _unitOfWork = _provider.GetRequiredService<IUnitOfWork>();
            _currentUserService = _provider.GetRequiredService<ICurrentUserService>();
        }


        public IAuthTokenService AuthTokenService => _authTokenService ??= new AuthTokenService(_config, _unitOfWork, _currentUserService);
        //public IReviewService ReviewService => _reviewService ??= new ReviewsService(_provider.GetRequiredService<IReviewRepository>());
        public IFileUploadManagementService FileUploadManagementService => _fileUploadManagementService ??= new FileUploadManagementService(_provider.GetRequiredService<IUnitOfWork>(), _provider.GetRequiredService<IFileStorageService>());
        public IBookingManagementService BookingManagementService =>
            _bookingManagementService ??= new BookingManagementService(
                _provider.GetRequiredService<IUnitOfWork>(),
                _provider.GetRequiredService<INotificationService>(),
                _provider.GetRequiredService<ICancellationService>(),
                _provider.GetRequiredService<ILogger<BookingManagementService>>());
        public ICheckoutManagementService CheckoutManagementService =>
            _checkoutManagementService ??= new StripeCheckoutService(
                _provider.GetRequiredService<StripeClient>(),
                _provider.GetRequiredService<IUnitOfWork>(),
                _provider.GetRequiredService<ILogger<StripeCheckoutService>>(),
                _config);
        public IConversationService ConversationService =>
    _conversationService ??= new ConversationService(
        _provider.GetRequiredService<IConversationRepository>(),
        _provider.GetRequiredService<IUnitOfWork>());   

        public IMessageService MessageService =>
            _messageService ??= new MessageService(
                _provider.GetRequiredService<IMessageRepository>(),
                _provider.GetRequiredService<IConversationRepository>(),
                _provider.GetRequiredService<IUnitOfWork>(),
                _provider.GetRequiredService<ILogger<MessageService>>());
        public IPropertyFilterExtractorService PropertyFilterExtractorService =>
                _propertyFilterExtractorService ??= new PropertyFilterExtractorService
                (_provider.GetRequiredService<ChatClient>(), _provider.GetRequiredService<IUnitOfWork>());
        public ICancellationService CancellationService => _provider.GetRequiredService<ICancellationService>();
        public IPaymentRefundService PaymentRefundService => _provider.GetRequiredService<IPaymentRefundService>();
        public IStripeTransferService StripeTransferService => _provider.GetRequiredService<IStripeTransferService>();
        public INotificationService NotificationService => _provider.GetRequiredService<INotificationService>();
        //public IReviewService ReviewsService => _provider.GetRequiredService<IReviewService>();
    }
}
