using Travellin.Travellin.Infrastructure.Services;

namespace Travellin.Travellin.Core.Interfaces
{
    public interface IServiceFactory
    {
        public IAuthTokenService AuthTokenService { get; }
    }
}
