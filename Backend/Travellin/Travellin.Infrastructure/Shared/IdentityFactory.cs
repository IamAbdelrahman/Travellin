using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Travellin.Travellin.Core.Interfaces;
namespace Travellin.Travellin.Infrastructure.Shared
{
    public class IdentityFactory : IIdentityFactory
    {
        private readonly IServiceProvider _provider;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<AppUser> _signInManager;

        public IdentityFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public UserManager<AppUser> UserManager => _userManager ??= _provider.GetRequiredService<UserManager<AppUser>>();

        public RoleManager<IdentityRole> RoleManager => _roleManager ??= _provider.GetRequiredService<RoleManager<IdentityRole>>();

        public SignInManager<AppUser> SignInManager => _signInManager ??= _provider.GetRequiredService<SignInManager<AppUser>>();
    }
}
