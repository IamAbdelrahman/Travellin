using Travellin.Travellin.Core.Enums;

namespace Travellin.Core.Entities
{
    public class GuestType : BaseEntity<int>
    {
        public string Name { get; set; } = TypeGuest.Adult.ToString();
        public virtual ICollection<PropertyGuest> PropertyGuests { get; set; } = new HashSet<PropertyGuest>();
    }
}
