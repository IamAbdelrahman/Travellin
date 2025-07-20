using Travellin.Core.Dtos.GuestTypes;

namespace Travellin.Core.Dtos.PropertyGuests
{
    public class PropertyGuestDto
    {
        public string PropertyId { get; set; }
        public GuestTypesDto GuestType { get; set; }
        public int GuestCount { get; set; }
    }
}
