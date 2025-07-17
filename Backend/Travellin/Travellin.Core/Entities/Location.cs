using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Travellin.Travellin.Core.Entities
{
    public class Location : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Address { get; set; }

        public Guid? CountryId { get; set; }

        public Guid? RegionId { get; set; }

        // Navigation properties
        public Country? Country { get; set; }
        public Region? Region { get; set; }
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
