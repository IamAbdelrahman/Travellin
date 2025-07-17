using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class PropertyType : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } // e.g., "EntireApartment", "PrivateRoom"

        // Navigation property
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
