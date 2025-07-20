using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Validation
{
    public class AllowedImageExtensionsAttribute : BaseImageValidationAttribute
    {
        public AllowedImageExtensionsAttribute(string[] allowedExtensions)
            : base(allowedExtensions)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (!IsValidExtension(file.FileName))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }
    }
}