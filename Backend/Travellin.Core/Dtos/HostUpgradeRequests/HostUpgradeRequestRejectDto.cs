using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.HostUpgradeRequests
{
    public class HostUpgradeRequestRejectDto
    {
        [Required]
        public string RejectionReason { get; set; }
    }
}
