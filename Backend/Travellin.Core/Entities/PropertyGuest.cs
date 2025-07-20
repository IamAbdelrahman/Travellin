namespace Travellin.Core.Entities
{
    public class PropertyGuest
    {
        public string PropertyId { get; set; }
        public int GuestTypeId { get; set; }
        public int GuestCount { get; set; }
        public virtual Property Property { get; set; }
        public virtual GuestType GuestType { get; set; }
    }
}
