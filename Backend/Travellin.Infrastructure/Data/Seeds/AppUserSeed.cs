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
                PasswordHash = "AQAAAAIAAYagAAAAEO/q6OSHKyNTnPIucWSWuAmTqfZHsqAMA+fnMfFPz28zoy4gwyv9Qy1QTjaAOCnJYg==",
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
                PasswordHash = "AQAAAAIAAYagAAAAEKPhsE1ZH2ywRVcOxNIhAIIfbvEEEUx9a0cKblC7AG3bUp7kBN57YBS6h4eiSpcieg==",
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
                PasswordHash = "AQAAAAIAAYagAAAAEMWTZVZgAJ/EsUyRjvSvhzLikb2SaCnhIAP7KuZmp8g7Gofn24rv/MdjHEUgNyB68w==",
                SecurityStamp = "GUESTSTAMP",
                ConcurrencyStamp = "4bc5ed7c-f23c-41b2-8f24-6cde1379cf70",
                PhoneNumberConfirmed = false,
            }
        };
    }
}
