using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class BookingGuestSeed
    {
        public static List<BookingGuest> Data => new()
        {
            new BookingGuest { BookingId = "7f6b0bb5-e99e-47c7-8d75-b5d46284e241", GuestTypeId = 1, GuestCount = 3 },
            new BookingGuest { BookingId = "49b69c8a-8b4b-4021-85f4-ff273b70c85d", GuestTypeId = 2, GuestCount = 3 },
            new BookingGuest { BookingId = "438d19e1-66fc-4219-9e3d-0519c9c27332", GuestTypeId = 3, GuestCount = 2 },
            new BookingGuest { BookingId = "e42b9075-d67c-4b5f-8316-bde33ef7272a", GuestTypeId = 1, GuestCount = 2 },
            new BookingGuest { BookingId = "b6d7b477-9b64-4a79-b7a3-b01c45378d5e", GuestTypeId = 2, GuestCount = 1 },
            new BookingGuest { BookingId = "0fe8f9f5-7751-460b-b39f-dab6946c0ba2", GuestTypeId = 2, GuestCount = 1 },
            new BookingGuest { BookingId = "7b479ff7-22c5-46ad-85a3-204b502e5d0b", GuestTypeId = 4, GuestCount = 2 },
            new BookingGuest { BookingId = "8a45a4b6-24ab-4a5b-8ef3-17b7de41295a", GuestTypeId = 1, GuestCount = 2 },
            new BookingGuest { BookingId = "d2bc71b9-d703-43fc-a90f-bf22f29a7b4e", GuestTypeId = 2, GuestCount = 2 }
        };
    }
}