using Travellin.Core.Entities;
using System.ComponentModel.DataAnnotations;
using Travellin.Core.Enums;

namespace Travellin.Core.Validation
{
    public class ValidHostUgradeRequestStatusAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Relax validation
            if (value is null)
                return ValidationResult.Success;

            // Assume null/empty checks are handled by [Required] if needed
            if (value is string stringValue)
            {
                if (Enum.TryParse(typeof(HostUgradeRequestStatus), stringValue, true, out _))
                    return ValidationResult.Success;
            }
            else if (value != null && Enum.IsDefined(typeof(HostUgradeRequestStatus), value))
            {
                return ValidationResult.Success;
            }

            // Invalid case
            var allowedValues = string.Join(", ", Enum.GetNames(typeof(HostUgradeRequestStatus)));
            return new ValidationResult($"Must be one of: {allowedValues}");
        }
    }
}
