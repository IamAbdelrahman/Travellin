namespace Travellin.Core.Entities
{
    public class PropertyAvailability : BaseEntity<int>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string PropertyId { get; set; }
        public virtual Property Property { get; set; }
    }
}
