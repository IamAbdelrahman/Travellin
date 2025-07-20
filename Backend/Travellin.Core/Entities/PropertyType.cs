namespace Travellin.Core.Entities
{
    public class PropertyType : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Icon { get; set; }
        public virtual ICollection<Property> Properties { get; set; } = new HashSet<Property>();
    }
}
