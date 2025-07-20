using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Validation
{
    public abstract class BaseImageValidationAttribute : ValidationAttribute
    {
        protected readonly string[] _allowedExtensions;

        protected BaseImageValidationAttribute(string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected bool IsValidExtension(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return _allowedExtensions.Contains(extension);
        }

        protected string GetErrorMessage()
        {
            return ErrorMessage ?? $"Only the following file types are allowed: {string.Join(", ", _allowedExtensions)}";
        }
    }
}