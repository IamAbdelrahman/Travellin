namespace Travellin.Core.Dtos.PropertySpaces
{
    public class PropertySpaceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsShared { get; set; } = false;
        public int PropertySpaceTypeId { get; set; }
        public string PropertyId { get; set; }
    }
}
