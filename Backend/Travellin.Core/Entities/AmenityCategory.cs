namespace Travellin.Core.Entities
{
    public class AmenityCategory : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Amenity> Amenities { get; set; } = new HashSet<Amenity>();
    }
}
