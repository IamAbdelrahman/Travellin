using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.Accounts
{
    public class RegisterDto
    {
        [EmailAddress]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(12, ErrorMessage = "Password must be at least 12 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Range(typeof(DateOnly), "1900-01-01", "2100-12-31", ErrorMessage = "Birth date must be between 1900 and 2100")]

        public DateOnly? BirthDate { get; set; }
    }
}