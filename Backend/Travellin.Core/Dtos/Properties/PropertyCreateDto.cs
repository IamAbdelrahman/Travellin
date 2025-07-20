using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.Properties
{
    public class PropertyCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters")]
        public string Title { get; set; }

        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Property type is required")]
        public int PropertyTypeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Location is required")]
        public int LocationId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal PricePerNight { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public decimal Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public decimal Longitude { get; set; }

        [StringLength(1000, ErrorMessage = "Safety info cannot exceed 1000 characters")]
        public string? SafteyInfo { get; set; }

        [StringLength(1000, ErrorMessage = "House rules cannot exceed 1000 characters")]
        public string? HouseRules { get; set; }

        [StringLength(1000, ErrorMessage = "Cancellation policy cannot exceed 1000 characters")]
        public string? CancellationPolicy { get; set; }
    }
}