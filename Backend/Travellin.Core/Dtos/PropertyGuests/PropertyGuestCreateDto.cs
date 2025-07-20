using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.PropertyGuests
{
    public class PropertyGuestCreateDto
    {
        [Required]
        public string PropertyId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "GuestTypeId must be greater than 1")]
        public int GuestTypeId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "GuestCount must be greater than 1")]
        public int GuestCount { get; set; }
    }
}
