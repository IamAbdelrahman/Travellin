using System.ComponentModel.DataAnnotations;

namespace Travellin.Travellin.Core.Entities
{
    public class PropertyImage : BaseEntity
    {
        public Guid PropertyId { get; set; }

        [Required, MaxLength(500)]
        public string ImageUrl { get; set; } //  points to Azure Blob Storage

        [MaxLength(200)]
        public string? Caption { get; set; }

        // Navigation property
        public Property Property { get; set; }
    }
}
