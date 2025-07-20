namespace Travellin.Core.Entities
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public int? CountryId { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? PhotoId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual FileUpload? Photo { get; set; }
        public virtual Country? Country { get; set; }
    }
}
