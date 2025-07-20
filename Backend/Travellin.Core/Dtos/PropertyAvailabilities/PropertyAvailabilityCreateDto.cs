using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.PropertyAvailabilities
{
    public class PropertyAvailabilityCreateDto
    {
        [Required]
        public string PropertyId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
