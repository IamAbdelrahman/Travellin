namespace Travellin.Core.Dtos.PropertyAvailabilities
{
    public class PropertyAvailabilityDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PropertyId { get; set; }
    }
}
