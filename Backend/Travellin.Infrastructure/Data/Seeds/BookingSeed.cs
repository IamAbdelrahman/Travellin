using Travellin.Core.Entities;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class BookingSeed
    {
        public static List<Booking> Data => new()
        {
            new Booking
            {
                Id = "7f6b0bb5-e99e-47c7-8d75-b5d46284e241",
                PropertyId = "cc4e48ea-ca54-4d32-a448-3c2c9d14f936",
                UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                CheckIn = new DateTime(2025, 5, 5),
                CheckOut = new DateTime(2025, 5, 10),
                PricePerNight = 3000,
                TotalFees = 89981,
                Status = BookingStatus.Pending,
                CreatedAt = new DateTime(2024, 12, 12),
                UpdatedAt = new DateTime(2025, 2, 10)
            },
            new Booking
            {
                Id = "49b69c8a-8b4b-4021-85f4-ff273b70c85d",
                PropertyId = "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4",
                UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                CheckIn = new DateTime(2025, 5, 5),
                CheckOut = new DateTime(2025, 5, 10),
                PricePerNight = 5000,
                TotalFees = 1200,
                Status = BookingStatus.Confirmed,
                CreatedAt = new DateTime(2024, 10, 12),
                UpdatedAt = new DateTime(2024, 12, 10)
            },
            new Booking
            {
                Id = "438d19e1-66fc-4219-9e3d-0519c9c27332",
                PropertyId = "3e7f99ab-228a-4d90-91c4-6adf8c12e048",
                UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                CheckIn = new DateTime(2025, 5, 15),
                CheckOut = new DateTime(2025, 5, 20),
                PricePerNight = 5000,
                TotalFees = 1200,
                Status = BookingStatus.Confirmed,
                CreatedAt = new DateTime(2024, 10, 12),
                UpdatedAt = new DateTime(2024, 12, 10)
            },
            new Booking
            {
                Id = "e42b9075-d67c-4b5f-8316-bde33ef7272a",
                PropertyId = "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa",
                UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                CheckIn = new DateTime(2025, 5, 15),
                CheckOut = new DateTime(2025, 5, 20),
                PricePerNight = 5000,
                TotalFees = 1200,
                Status = BookingStatus.Confirmed,
                CreatedAt = new DateTime(2024, 10, 12),
                UpdatedAt = new DateTime(2024, 12, 10)
            },
            new Booking
            {
                Id = "b6d7b477-9b64-4a79-b7a3-b01c45378d5e",
                PropertyId = "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2",
                UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                CheckIn = new DateTime(2025, 5, 15),
                CheckOut = new DateTime(2025, 5, 20),
                PricePerNight = 1000,
                TotalFees = 1200,
                Status = BookingStatus.Cancelled,
                CreatedAt = new DateTime(2024, 10, 12),
                UpdatedAt = new DateTime(2024, 12, 10)
            },
            new Booking
            {
                Id = "0fe8f9f5-7751-460b-b39f-dab6946c0ba2",
                PropertyId = "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3",
                UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                CheckIn = new DateTime(2025, 5, 15),
                CheckOut = new DateTime(2025, 5, 20),
                PricePerNight = 2120,
                TotalFees = 1900,
                Status = BookingStatus.Confirmed,
                CreatedAt = new DateTime(2024, 10, 12),
                UpdatedAt = new DateTime(2024, 12, 10)
            },
            new Booking
            {
                Id = "7b479ff7-22c5-46ad-85a3-204b502e5d0b",
                PropertyId = "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7",
                UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                CheckIn = new DateTime(2025, 5, 15),
                CheckOut = new DateTime(2025, 5, 20),
                PricePerNight = 3000,
                TotalFees = 1900,
                Status = BookingStatus.Pending,
                CreatedAt = new DateTime(2024, 10, 12),
                UpdatedAt = new DateTime(2024, 12, 10)
            },
            new Booking
            {
                Id = "8a45a4b6-24ab-4a5b-8ef3-17b7de41295a",
                PropertyId = "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f",
                UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                CheckIn = new DateTime(2025, 5, 15),
                CheckOut = new DateTime(2025, 5, 20),
                PricePerNight = 3000,
                TotalFees = 1900,
                Status = BookingStatus.Cancelled,
                CreatedAt = new DateTime(2024, 10, 12),
                UpdatedAt = new DateTime(2024, 12, 10)
            },
            new Booking
            {
                Id = "d2bc71b9-d703-43fc-a90f-bf22f29a7b4e",
                PropertyId = "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f",
                UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                CheckIn = new DateTime(2025, 6, 15),
                CheckOut = new DateTime(2025, 7, 20),
                PricePerNight = 3000,
                TotalFees = 2000,
                Status = BookingStatus.Confirmed,
                CreatedAt = new DateTime(2024, 10, 12),
                UpdatedAt = new DateTime(2024, 12, 10)
            }
        };
    }
}