using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class AppUserSeed
    {
        public static List<AppUser> Data => new()
        {
            new AppUser
            {
                Id = "2dacdb51-fee9-4479-904c-cafe7dca22a6",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEC7spMPg5RTE/+JwhaFMZ9D4qe125yj/pgHQRdpqvzZn/yUZ56sxPK6NYZ+WPproog==",
                SecurityStamp = "2O776OTQMPGHNUKGKGVD7EK56EWEHWJ4",
                ConcurrencyStamp = "2bc5ed7c-f23c-41b2-8f24-6cde1379cf70",
                PhoneNumberConfirmed = false,
            },
            new AppUser
            {
                Id = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                UserName = "host",
                NormalizedUserName = "HOST",
                Email = "host@email.com",
                NormalizedEmail = "HOST@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEC7spMPg5RTE/+JwhaFMZ9D4qe125yj/pgHQRdpqvzZn/yUZ56sxPK6NYZ+WPproog==",
                SecurityStamp = "HOSTSTAMP",
                ConcurrencyStamp = "3bc5ed7c-f23c-41b2-8f24-6cde1379cf70",
                PhoneNumberConfirmed = false,
            },
            new AppUser
            {
                Id = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                UserName = "guest",
                NormalizedUserName = "GUEST",
                Email = "guest@email.com",
                NormalizedEmail = "GUEST@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEC7spMPg5RTE/+JwhaFMZ9D4qe125yj/pgHQRdpqvzZn/yUZ56sxPK6NYZ+WPproog==",
                SecurityStamp = "GUESTSTAMP",
                ConcurrencyStamp = "4bc5ed7c-f23c-41b2-8f24-6cde1379cf70",
                PhoneNumberConfirmed = false,
            }
        };
    }
}
