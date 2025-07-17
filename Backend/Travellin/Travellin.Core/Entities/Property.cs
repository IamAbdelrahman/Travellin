using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class Property : BaseEntity
    {
        public Guid HostId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required, MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }
        public Guid PropertyTypeId { get; set; }

        public Guid LocationId { get; set; }

        [Required]
        public decimal BasePrice { get; set; } // USD, per night

        public decimal Rating { get; set; } // Average from reviews

        public bool IsBlocked { get; set; } // Blocked due to negative reviews

        // Navigation properties
        public User Host { get; set; }
        public PropertyType PropertyType { get; set; }
        public Location Location { get; set; }
        public ICollection<PropertyAmenity> Amenities { get; set; } = new List<PropertyAmenity>();
        public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
        public ICollection<PropertyAvailability> AvailabilityBlocks { get; set; } = new List<PropertyAvailability>();
        public ICollection<PropertyFee> Fees { get; set; } = new List<PropertyFee>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public ICollection<Violation> Violations { get; set; } = new List<Violation>(); // Violations against this property
    }
}

