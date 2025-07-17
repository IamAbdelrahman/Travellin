using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nestin.Core.Interfaces;
using Nestin.Infrastructure.Services;
using OpenAI.Chat;
using Stripe;
using Travellin.Travellin.Infrastructure.Services;

namespace Travellin.Travellin.Infrastructure.Shared
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _provider;
        private readonly IConfiguration _config;

        private IAuthTokenService _authTokenService;
        private IFileUploadManagementService _fileUploadManagementService;
        private IBookingManagementService _bookingManagementService;
        private IPropertyFilterExtractorService _propertyFilterExtractorService;
        private ICheckoutManagementService _checkoutManagementService;

        public ServiceFactory(IServiceProvider provider, IConfiguration config)
        {
            _provider = provider;
            _config = config;
        }

        public IAuthTokenService AuthTokenService => _authTokenService ??= new AuthTokenService(_config);
        public IFileUploadManagementService FileUploadManagementService => _fileUploadManagementService ??= new FileUploadManagementService(_provider.GetRequiredService<IUnitOfWork>(), _provider.GetRequiredService<IFileStorageService>());
        public IBookingManagementService BookingManagementService =>
            _bookingManagementService ??= new BookingManagementService(_provider.GetRequiredService<IUnitOfWork>());
        public IPropertyFilterExtractorService PropertyFilterExtractorService =>
            _propertyFilterExtractorService ??= new PropertyFilterExtractorService
                (_provider.GetRequiredKeyedService<ChatClient>("MainOpenAIClient"), _provider.GetRequiredService<IUnitOfWork>());

        public ICheckoutManagementService CheckoutManagementService =>
            _checkoutManagementService ??= new StripeCheckoutService(
                _provider.GetRequiredService<StripeClient>(),
                _provider.GetRequiredService<IUnitOfWork>(),
                _provider.GetRequiredService<ILogger<StripeCheckoutService>>(),
                _config);
    }
}
