using Travellin.Travellin.Core.Enums;

namespace Travellin.Core.Entities
{
    public class CancellationPolicy : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DaysBeforeCheckIn { get; set; } // Days before check-in when policy applies
        public decimal RefundPercentage { get; set; } // Percentage of total amount to refund
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<Property> Properties { get; set; } = new HashSet<Property>();
    }
} 