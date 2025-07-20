using Travellin.Core.Validation;

namespace Travellin.Core.Dtos.HostUpgradeRequests
{
    public class HostUpgradeRequestFilterQueryParamsDto : GetAllQueryDto
    {
        public string? UserId { get; set; }
        [ValidHostUgradeRequestStatus]
        public string? Status { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        [ValidHostUpgradeDocumentNumber]
        public string? DocumentNumber { get; set; }
        public string? Sort { get; set; }
    }
}
