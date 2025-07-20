namespace Travellin.Core.Entities
{
    public class Location : BaseEntity<int>
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public ICollection<Property> Properties { get; set; } = new HashSet<Property>();
    }
}
