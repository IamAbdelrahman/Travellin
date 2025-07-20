using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class ReviewSeed
    {
        public static List<Review> Data => new()
        {
            new Review
            {
                Id = "66d2f0d9-1f1f-4a02-81d6-0ecabc5215e6",
                BookingId = "7f6b0bb5-e99e-47c7-8d75-b5d46284e241",
                Comment = "Great stay overall",
                Cleanliness = 4.5m,
                Accuracy = 4.0m,
                CheckIn = 5.0m,
                Communication = 4.5m,
                Location = 4.0m,
                Value = 5.0m,
                CreatedAt = DateTime.Parse("2025-05-16"),
                UpdatedAt = DateTime.Parse("2025-05-17")
            },
            new Review
            {
                Id = "a54b86b1-65e2-426b-81ef-c65c71e5b8d0",
                BookingId = "49b69c8a-8b4b-4021-85f4-ff273b70c85d",
                Comment = "Comfortable and clean",
                Cleanliness = 5.0m,
                Accuracy = 4.5m,
                CheckIn = 4.0m,
                Communication = 5.0m,
                Location = 4.5m,
                Value = 4.5m,
                CreatedAt = DateTime.Parse("2025-05-16"),
                UpdatedAt = DateTime.Parse("2025-05-17")
            },
            new Review
            {
                Id = "2fca2c7e-263b-4d7e-99e7-0c1c3ad2aa08",
                BookingId = "438d19e1-66fc-4219-9e3d-0519c9c27332",
                Comment = "Could be better",
                Cleanliness = 3.5m,
                Accuracy = 3.0m,
                CheckIn = 2.5m,
                Communication = 4.0m,
                Location = 3.0m,
                Value = 3.0m,
                CreatedAt = DateTime.Parse("2025-05-16"),
                UpdatedAt = DateTime.Parse("2025-05-17")
            },
            new Review
            {
                Id = "fca2e08b-0436-4f3f-8261-f69cf3eaa579",
                BookingId = "e42b9075-d67c-4b5f-8316-bde33ef7272a",
                Comment = "Average experience",
                Cleanliness = 3.0m,
                Accuracy = 3.5m,
                CheckIn = 3.0m,
                Communication = 3.5m,
                Location = 3.0m,
                Value = 3.0m,
                CreatedAt = DateTime.Parse("2025-05-16"),
                UpdatedAt = DateTime.Parse("2025-05-17")
            },
            new Review
            {
                Id = "72b3d68d-234a-4ed7-b7f7-e07fc82f58ef",
                BookingId = "b6d7b477-9b64-4a79-b7a3-b01c45378d5e",
                Comment = "Good value for money",
                Cleanliness = 4.0m,
                Accuracy = 4.0m,
                CheckIn = 3.5m,
                Communication = 4.5m,
                Location = 4.0m,
                Value = 4.0m,
                CreatedAt = DateTime.Parse("2025-05-16"),
                UpdatedAt = DateTime.Parse("2025-05-17")
            },
            new Review
            {
                Id = "ffc234ae-2820-4fd6-b9d7-6b315d91a790",
                BookingId = "0fe8f9f5-7751-460b-b39f-dab6946c0ba2",
                Comment = "Perfect location",
                Cleanliness = 5.0m,
                Accuracy = 5.0m,
                CheckIn = 5.0m,
                Communication = 5.0m,
                Location = 5.0m,
                Value = 5.0m,
                CreatedAt = DateTime.Parse("2025-05-16"),
                UpdatedAt = DateTime.Parse("2025-05-17")
            },
            new Review
            {
                Id = "e62cd505-8d60-430b-8b52-16d40902a303",
                BookingId = "7b479ff7-22c5-46ad-85a3-204b502e5d0b",
                Comment = "Very good host",
                Cleanliness = 4.5m,
                Accuracy = 4.5m,
                CheckIn = 4.0m,
                Communication = 5.0m,
                Location = 4.5m,
                Value = 4.5m,
                CreatedAt = DateTime.Parse("2025-05-16"),
                UpdatedAt = DateTime.Parse("2025-05-17")
            },
            new Review
            {
                Id = "84c03e84-cd8b-4dbf-a0f4-48ed3dd0b0aa",
                BookingId = "8a45a4b6-24ab-4a5b-8ef3-17b7de41295a",
                Comment = "Excellent experience",
                Cleanliness = 5.0m,
                Accuracy = 5.0m,
                CheckIn = 5.0m,
                Communication = 5.0m,
                Location = 5.0m,
                Value = 5.0m,
                CreatedAt = DateTime.Parse("2025-05-16"),
                UpdatedAt = DateTime.Parse("2025-05-17")
            }
        };
    }
}