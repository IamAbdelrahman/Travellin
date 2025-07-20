using Microsoft.AspNetCore.Http;
using Travellin.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.PropertyPhotos
{
    public class PropertyPhotosUploadDto
    {
        [Required]
        public string PropertyId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one photo is required")]
        [AllowedImagesExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" },
            ErrorMessage = "Only image files (.jpg, .jpeg, .png, .gif) are allowed.")]
        public List<IFormFile> Photos { get; set; }
    }
}