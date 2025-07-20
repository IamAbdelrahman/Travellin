namespace Travellin.Core.Entities
{
    public class PropertyFee : BaseEntity<int>
    {
        public string PropertyId;
        public string Name;

        public decimal Amount { get; set; }

        public virtual Property Property { get; set; }
    }
}
