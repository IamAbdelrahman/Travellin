using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Shared;
using Travellin.Travellin.Core.Enums;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Infrastructure.Services
{
    public class AuthTokenService : IAuthTokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;
        private readonly string _audiance;
        private readonly int _expirationInDays;
        private readonly CookieOptions _cookieOptions;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        public AuthTokenService(IConfiguration config, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            // JWT Configs
            var signingKey = config["Jwt:SigningKey"];
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            _issuer = config["Jwt:Issuer"];
            _audiance = config["Jwt:Audience"];
            _expirationInDays = int.Parse(config["Jwt:ExpirationInDays"]);

            // Cookie configuration
            _cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(_expirationInDays),
                IsEssential = true
            };
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }


        public string CreateToken(AppUser user)
        {
            // Retrieve user roles using RoleManager
            var userRoles = user.Roles;

            // Set claims (the information we want to store in the JWT)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // Add each role as a claim
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            // Get the JWT secret key and issuer from configuration
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            // Set expiration for the token
            var expiration = DateTime.UtcNow.AddDays(_expirationInDays);

            // Create the token
            var token = new JwtSecurityToken(
                issuer: _issuer,       // Get issuer from configuration
                audience: _audiance,   // Get audience from configuration
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            // Return the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void SetAccessTokenCookie(HttpContext ctx, string token)
        {
            ctx.Response.Cookies.Append("access_token", token, _cookieOptions);
        }

        public void UnsetAccessTokenCookie(HttpContext ctx)
        {
            ctx.Response.Cookies.Delete("access_token", _cookieOptions);
        }

        public async Task EnsureEntityOwnershipAsync(string entityId, string userId, string errorMsg, AuthRoles role)
        {
            if (_currentUser == null)
                throw new UnauthorizedAccessException("User context not found");

            var entity = await _unitOfWork.PropertyRepository.GetByIdAsync(entityId);
            if (entity == null)
                throw new NotFoundException("Property not found");

            bool isAdmin = _currentUser.IsInRole("Admin");
            bool isHost = _currentUser.IsInRole("Host");
            bool isGuest = _currentUser.IsInRole("Guest");

            bool result = (role == AuthRoles.Admin && isAdmin) ||
                             (role == AuthRoles.Host && isHost) ||
                             (role == (AuthRoles.Admin | AuthRoles.Host) && (isAdmin || isHost)) ||
                             (role == AuthRoles.Guest && isGuest);

            if (result) return;

            if (entity.OwnerId != userId || !result)
                throw new ForbiddenException(errorMsg);
        }
    }
}
