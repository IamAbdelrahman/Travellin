using Travellin.Core.Dtos.Reviews;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class ReviewMappingExtenstions
    {
        public static ReviewDto ToDto(this Review entity)
        {
            return new ReviewDto
            {
                BookingId = entity.BookingId,
                Comment = entity.Comment,
                Cleanliness = entity.Cleanliness,
                Accuracy = entity.Accuracy,
                CheckIn = entity.CheckIn,
                Communication = entity.Communication,
                Location = entity.Location,
                Value = entity.Value,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Reviewer = entity.Booking?.User == null ? null : new Reviewer
                {
                    Id = entity.Booking.User.Id,
                    FirstName = entity.Booking.User?.UserProfile?.FirstName,
                    LastName = entity.Booking.User?.UserProfile?.LastName,
                    PhotoUrl = entity.Booking.User?.UserProfile?.Photo?.Path?.ToFullUrl()
                }
            };
        }
    }
}
