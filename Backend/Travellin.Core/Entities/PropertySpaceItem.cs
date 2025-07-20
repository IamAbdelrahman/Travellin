namespace Travellin.Core.Entities
{
    public class PropertySpaceItem : BaseEntity<int>
    {
        public int? PropertySpaceItemTypeId { get; set; }
        public string PropertySpaceId { get; set; }
        public int Quantity { get; set; }
        public virtual PropertySpaceItemType PropertySpaceItemType { get; set; }
        public virtual PropertySpace PropertySpace { get; set; }
    }
}
