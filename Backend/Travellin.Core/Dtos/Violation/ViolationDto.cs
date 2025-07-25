using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.Violations
{
    public class ViolationDto
    {
        public int Id { get; set; }
        public Guid ReportedById { get; set; }
        public string ReportedByName { get; set; }
        public string ReportedByAvatar { get; set; }
        public Guid? ReportedPropertyId { get; set; }
        public string? ReportedPropertyTitle { get; set; }
        public Guid? ReportedUserId { get; set; }
        public string? ReportedUserName { get; set; }
        public string ViolationType { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string? AdminNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }

    public class CreateViolationDto
    {
        [Required]
        public Guid? ReportedPropertyId { get; set; }

        [Required]
        public Guid? ReportedUserId { get; set; }

        [Required]
        public string ViolationType { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public List<string>? EvidenceUrls { get; set; }
    }

    public class UpdateViolationStatusDto
    {
        [Required]
        public string Status { get; set; }

        public string? AdminNotes { get; set; }
    }
}