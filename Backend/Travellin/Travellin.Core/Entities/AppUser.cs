using Microsoft.AspNetCore.Identity;

namespace Travellin.Travellin.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<IdentityRole> Roles { get; set; } = new HashSet<IdentityRole>();
        public virtual ICollection<Property> Properties { get; set; } = new HashSet<Property>();
        public virtual User? User { get; set; }
    }
}
