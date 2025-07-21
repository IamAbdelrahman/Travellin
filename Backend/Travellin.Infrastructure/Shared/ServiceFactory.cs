using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Services;
using Stripe;

namespace Travellin.Infrastructure.Shared
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _provider;
        private readonly IConfiguration _config;

        private IAuthTokenService _authTokenService;
        private IFileUploadManagementService _fileUploadManagementService;
        private IBookingManagementService _bookingManagementService;
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
        public ICheckoutManagementService CheckoutManagementService =>
            _checkoutManagementService ??= new StripeCheckoutService(
                _provider.GetRequiredService<StripeClient>(),
                _provider.GetRequiredService<IUnitOfWork>(),
                _provider.GetRequiredService<ILogger<StripeCheckoutService>>(),
                _config);
    }
}
