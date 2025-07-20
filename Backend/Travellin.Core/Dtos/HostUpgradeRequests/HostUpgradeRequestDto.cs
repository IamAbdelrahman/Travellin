namespace Travellin.Core.Dtos.HostUpgradeRequests
{
    public class HostUpgradeRequestDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? RejectionReason { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public PhotoDto FrontPhoto { get; set; }
        public PhotoDto BackPhoto { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
