using Travellin.Core.Dtos.BookingGuests;
using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.Bookings
{
    public class CreateBookingDto : IValidatableObject
    {
        [Required]
        public string PropertyId { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime Checkout { get; set; }
        public List<CreateBookingGuestDto> Guests { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (!Guid.TryParse(PropertyId, out var parsedGuid))
            {
                errors.Add(new ValidationResult("Invalid PropertyId format. Must be a valid GUID.", new[] { nameof(PropertyId) }));
            }


            if (CheckIn <= DateTime.UtcNow)
            {
                errors.Add(new ValidationResult("Check-in date must be in the future.", new[] { nameof(CheckIn) }));
            }

            if (Checkout <= DateTime.UtcNow)
            {
                errors.Add(new ValidationResult("Checkout date must be in the future.", new[] { nameof(Checkout) }));
            }

            if (CheckIn >= Checkout)
            {
                errors.Add(new ValidationResult("Checkout date must be after check-in date.", new[] { nameof(Checkout), nameof(CheckIn) }));
            }

            return errors;
        }
    }
}
