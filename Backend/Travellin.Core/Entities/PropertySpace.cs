namespace Travellin.Core.Entities
{
    public class PropertySpace : BaseEntity<string>
    {
        public string Name { get; set; }
        public int PropertySpaceTypeId { get; set; }
        public string PropertyId { get; set; }
        public bool IsShared { get; set; } = false;
        public virtual PropertySpaceType PropertySpaceType { get; set; }
        public virtual Property Property { get; set; }
        public virtual ICollection<PropertySpaceItem> PropertySpaceItems { get; set; } = new HashSet<PropertySpaceItem>();
    }
}
