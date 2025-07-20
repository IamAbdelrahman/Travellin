using Microsoft.AspNetCore.Http;
using Travellin.Core.Entities;

namespace Travellin.Core.Interfaces
{
    public interface IAuthTokenService
    {
        public void SetAccessTokenCookie(HttpContext ctx, string token);
        public void UnsetAccessTokenCookie(HttpContext ctx);
        public string CreateToken(AppUser user);
    }
}
