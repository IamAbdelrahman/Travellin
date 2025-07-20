using Travellin.Core.Dtos.BookingGuests;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class BookingGuestMappingExtenstions
    {
        public static BookingGuestDto ToDto(this BookingGuest bookingGuest)
        {
            return new BookingGuestDto
            {
                BookingId = bookingGuest.BookingId,
                GuestTypeId = bookingGuest.GuestTypeId,
                GuestCount = bookingGuest.GuestCount
            };
        }
    }
}
