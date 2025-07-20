using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Travellin.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<IdentityRole> Roles { get; set; } = new HashSet<IdentityRole>();
        public virtual ICollection<Property> Properties { get; set; } = new HashSet<Property>();
        public virtual UserProfile? UserProfile { get; set; }
    }
}
