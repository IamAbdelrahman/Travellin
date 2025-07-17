using System.ComponentModel.DataAnnotations;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Travellin.Core.Entities
{
    public class PropertyFee : BaseEntity
    {
        public Guid PropertyId { get; set; }

        [Required, MaxLength(50)]
        public string FeeType { get; set; } = FeeTypeStatus.Cleaning.ToString(); // e.g., "Cleaning", "Service"

        [Required]
        public decimal Amount { get; set; } // USD

        // Navigation property
        public Property Property { get; set; }
    }
}
