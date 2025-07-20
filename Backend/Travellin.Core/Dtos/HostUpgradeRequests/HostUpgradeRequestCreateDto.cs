using Microsoft.AspNetCore.Http;
using Travellin.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace Travellin.Core.Dtos.HostUpgradeRequests
{
    public class HostUpgradeRequestCreateDto
    {
        [Required]
        [ValidHostUpgradeDocumentType]
        public string DocumentType { get; set; }
        [Required]
        [ValidHostUpgradeDocumentNumber]
        public string DocumentNumber { get; set; }
        [Required]
        [AllowedImageExtensionsAttribute(new[] { ".jpg", ".jpeg", ".png", ".gif" }, ErrorMessage = "Only image files (.jpg, .jpeg, .png, .gif) are allowed.")]
        public IFormFile FrontPhoto { get; set; }
        [Required]
        [AllowedImageExtensionsAttribute(new[] { ".jpg", ".jpeg", ".png", ".gif" }, ErrorMessage = "Only image files (.jpg, .jpeg, .png, .gif) are allowed.")]
        public IFormFile BackPhoto { get; set; }
    }
}
