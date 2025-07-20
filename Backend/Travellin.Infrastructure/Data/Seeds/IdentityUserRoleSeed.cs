using Microsoft.AspNetCore.Identity;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class IdentityUserRoleSeed
    {
        public static List<IdentityUserRole<string>> Data => new()
        {
            new IdentityUserRole<string>
            {
                UserId = "2dacdb51-fee9-4479-904c-cafe7dca22a6", // admin
                RoleId = "d35a86a5-72b3-4e7e-bb7f-5ef782b36f7c"  // Admin role ID
            },
            new IdentityUserRole<string>
            {
                UserId = "3dacdb51-fee9-4479-904c-cafe7dca22a7", // host
                RoleId = "59ebef1f-d79b-4db0-9c5a-304836f14ff1"  // Host role ID
            },
            new IdentityUserRole<string>
            {
                UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8", // guest
                RoleId = "9c75a5df-20a4-4ff1-85a5-bb52f9cf223f"  // Guest role ID
            }
        };
    }
}
