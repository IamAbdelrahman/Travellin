using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Validation
{
    public class AllowedImagesExtensionsAttribute : BaseImageValidationAttribute
    {
        public AllowedImagesExtensionsAttribute(string[] allowedExtensions)
            : base(allowedExtensions)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IEnumerable<IFormFile> files)
            {
                foreach (var file in files)
                {
                    if (!IsValidExtension(file.FileName))
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}