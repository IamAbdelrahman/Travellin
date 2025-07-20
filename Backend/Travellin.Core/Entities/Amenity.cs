namespace Travellin.Core.Entities
{
    public class Amenity : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Icon { get; set; }
        public int CategoryId { get; set; }
        public virtual AmenityCategory AmenityCategory { get; set; }
    }
}
