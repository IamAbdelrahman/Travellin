using Travellin.Core.Enums;

namespace Travellin.Core.Entities
{
    public class PropertyPhoto
    {
        public string PhotoId { get; set; }
        public string PropertyId { get; set; }
        public string QualityStatus { get; set; } = QualityType.HighQuality.ToString(); // e.g., "HighQuality", "Blurry", "Dark", "Irrelevant"
        public string QualityFeedback { get; set; } // e.g., "Image is blurry, please upload a clearer photo"
        public DateTime TouchedAt { get; set; }
        public virtual Property Property { get; set; }
        public virtual FileUpload FileUpload { get; set; }
    }
}
