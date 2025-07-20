using Travellin.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Travellin.Core.Enums;

namespace Travellin.Core.Validation
{
    public class ValidHostUpgradeDocumentNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var documentNumber = value as string;
            if (string.IsNullOrWhiteSpace(documentNumber))
            {
                return ValidationResult.Success;
            }

            // Get the DocumentType value from the model
            var instance = validationContext.ObjectInstance;
            var documentTypeProperty = instance.GetType().GetProperty("DocumentType");
            var documentTypeValue = documentTypeProperty?.GetValue(instance) as string;

            // Relaxed behavior: if DocumentType is not present, skip validation
            if (string.IsNullOrWhiteSpace(documentTypeValue))
                return ValidationResult.Success;

            if (Enum.TryParse(typeof(HostUpgradeRequestDocumentType), documentTypeValue, true, out var documentType))
            {
                switch ((HostUpgradeRequestDocumentType)documentType)
                {
                    case HostUpgradeRequestDocumentType.Passport:
                        // Example passport validation (adjust according to your requirements)
                        if (documentNumber.Length < 8 || documentNumber.Length > 20)
                        {
                            return new ValidationResult("Passport number must be between 8 and 20 characters.");
                        }
                        break;

                    case HostUpgradeRequestDocumentType.NationalId:
                        // Example national ID validation (adjust according to your country's format)
                        if (!Regex.IsMatch(documentNumber, @"^[A-Za-z0-9]{6,12}$"))
                        {
                            return new ValidationResult("National ID must be 6-12 alphanumeric characters.");
                        }
                        break;

                    default:
                        return new ValidationResult("Unknown document type.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
