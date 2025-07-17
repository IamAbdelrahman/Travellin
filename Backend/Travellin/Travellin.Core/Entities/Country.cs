using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class Country : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(3)]
        public string Code { get; set; } // e.g., "USA"

        // Navigation property
        public ICollection<Location> Locations { get; set; } = new List<Location>();
    }
}
