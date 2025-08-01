using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.UserProfilesDto
{
    public class UserProfileQueryParamsDto : GetAllQueryDto
    {
        public string? UserId { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
