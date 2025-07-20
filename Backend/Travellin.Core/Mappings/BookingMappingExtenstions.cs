using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class BookingMappingExtenstions
    {
        public static BookingDto ToDto(this Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                PricePerNight = booking.PricePerNight,
                TotalFees = booking.TotalFees,
                TotalAmount = booking.TotalAmount,
                Status = booking.Status.ToString(),
                CreatedAt = booking.CreatedAt,
                UpdatedAt = booking.UpdatedAt,
                BookingGuests = booking.BookingGuests.Select(x => x.ToDto()).ToList(),
                Property = booking.Property.ToPropertyListItemDto()
            };
        }
    }
}
