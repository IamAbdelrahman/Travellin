using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class Promotion : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Code { get; set; } // e.g., "SUMMER25"

        [Required, MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public decimal DiscountPercentage { get; set; } // e.g., 10.0 for 10%

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        // Navigation property
        public ICollection<UserUsedPromotion> UserUsedPromotions { get; set; } = new List<UserUsedPromotion>();
    }
}
