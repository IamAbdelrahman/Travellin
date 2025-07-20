using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Travellin.Core.Entities;

namespace Travellin.Core.Interfaces
{
    public interface IIdentityFactory
    {
        public UserManager<AppUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public SignInManager<AppUser> SignInManager { get; }
    }
}
