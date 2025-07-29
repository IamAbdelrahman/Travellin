using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenAI.Chat;
using Stripe;
using Travellin.Core.Interfaces;
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
        private readonly ILogger<StripeCheckoutService> _logger;

        public ServiceFactory(IServiceProvider provider, IConfiguration config)
        {
            _provider = provider;
            _config = config;
            _unitOfWork = _provider.GetRequiredService<IUnitOfWork>();
            _currentUserService = _provider.GetRequiredService<ICurrentUserService>();
        }


        public IAuthTokenService AuthTokenService => _authTokenService ??= new AuthTokenService(_config, _unitOfWork, _currentUserService);
        public IFileUploadManagementService FileUploadManagementService => _fileUploadManagementService ??= new FileUploadManagementService(_provider.GetRequiredService<IUnitOfWork>(), _provider.GetRequiredService<IFileStorageService>());
        public IBookingManagementService BookingManagementService =>
            _bookingManagementService ??= new BookingManagementService(_provider.GetRequiredService<IUnitOfWork>());
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
                (_provider.GetRequiredKeyedService<ChatClient>("MainOpenAIClient"), _provider.GetRequiredService<IUnitOfWork>());

    }
}
