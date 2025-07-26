using Microsoft.AspNetCore.Http;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Enums;
namespace Travellin.Core.Interfaces
{
    public interface IAuthTokenService
    {
        public void SetAccessTokenCookie(HttpContext ctx, string token);
        public void UnsetAccessTokenCookie(HttpContext ctx);
        public string CreateToken(AppUser user);
        public Task EnsureEntityOwnershipAsync(string entityId, string userId, string errorMsg, AuthRoles role);

    }
}
