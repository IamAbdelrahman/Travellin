using Travellin.Core.Entities;
using Travellin.Travellin.Core.Enums;
namespace Travellin.Core.Dtos.Bookings
{
    public class GetAllBookingsQueryParamsDto : GetAllQueryDto
    {
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string? Status { get; set; }
    }

    public static class GetAllBookingsQueryParamsDtoExtensions
    {
        public static BookingStatus? GetStatusAsEnum(this GetAllBookingsQueryParamsDto queryParams)
        {
            if (string.IsNullOrWhiteSpace(queryParams.Status))
            {
                return null;
            }

            if (Enum.TryParse<BookingStatus>(queryParams.Status, ignoreCase: true, out var status))
            {
                return status;
            }

            return null;
        }
    }
}
