namespace Travellin.Core.Entities
{
    public class PropertyPhoto
    {
        public string PhotoId { get; set; }
        public string PropertyId { get; set; }
        public DateTime TouchedAt { get; set; }
        public virtual Property Property { get; set; }
        public virtual FileUpload FileUpload { get; set; }
    }
}
