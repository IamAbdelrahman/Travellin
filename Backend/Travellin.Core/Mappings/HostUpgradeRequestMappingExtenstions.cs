using Travellin.Core.Dtos.HostUpgradeRequests;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class HostUpgradeRequestMappingExtenstions
    {
        public static HostUpgradeRequestDto ToDto(this HostUpgradeRequest entity)
        {
            return new HostUpgradeRequestDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Status = entity.Status.ToString(),
                ApprovedBy = entity.ApprovedBy,
                ApprovalDate = entity.ApprovalDate,
                RejectionReason = entity.RejectionReason,
                DocumentType = entity.DocumentType.ToString(),
                DocumentNumber = entity.DocumentNumber,
                FrontPhoto = entity.FrontPhoto?.ToDto(),
                BackPhoto = entity.BackPhoto?.ToDto(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
