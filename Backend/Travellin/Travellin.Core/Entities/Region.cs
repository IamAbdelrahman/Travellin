using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class Region : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public Guid CountryId { get; set; }

        // Navigation properties
        public Country Country { get; set; }
        public ICollection<Location> Locations { get; set; } = new List<Location>();
    }
}
