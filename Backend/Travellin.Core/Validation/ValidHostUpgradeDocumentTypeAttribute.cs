using Travellin.Core.Entities;
using System.ComponentModel.DataAnnotations;
using Travellin.Core.Enums;

namespace Travellin.Core.Validation
{
    public class ValidHostUpgradeDocumentTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var documentType = value.ToString();
            if (Enum.TryParse(typeof(HostUpgradeRequestDocumentType), documentType, true, out _))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Invalid DocumentType. Allowed values are: {string.Join(", ", Enum.GetNames(typeof(HostUpgradeRequestDocumentType)))}");
        }
    }
}
