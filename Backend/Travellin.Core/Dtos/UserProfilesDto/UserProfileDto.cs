using Travellin.Core.Dtos.Countires;

namespace Travellin.Core.Dtos.UserProfilesDto
{
    public class UserProfileDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public DateOnly? BirthDate { get; set; }
        public CountryDto? Country { get; set; }
        public UserProfilePhotoDto? Photo { get; set; }
    }
}
