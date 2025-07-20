namespace Travellin.Core.Entities
{
    public class PropertySpaceType : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<PropertySpaceItemType> PropertySpaceItemTypes { get; set; } = new HashSet<PropertySpaceItemType>();
        public virtual ICollection<PropertySpace> PropertySpaces { get; set; } = new HashSet<PropertySpace>();
    }
}
