using Microsoft.AspNetCore.Identity;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class IdentityRoleSeed
    {
        public static List<IdentityRole> Data => new()
        {
            new IdentityRole
            {
                Id = "d35a86a5-72b3-4e7e-bb7f-5ef782b36f7c",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "9c75a5df-20a4-4ff1-85a5-bb52f9cf223f",
                Name = "Guest",
                NormalizedName = "GUEST"
            },
            new IdentityRole
            {
                Id = "59ebef1f-d79b-4db0-9c5a-304836f14ff1",
                Name = "Host",
                NormalizedName = "HOST"
            }
        };
    }
}
