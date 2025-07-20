using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Travellin.Core.Dtos;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class FileUploadPathMappingExtensions
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static string _configuredBaseUrl;

        public static void Init(IConfiguration configuration, IHttpContextAccessor httpContextAccessor = null)
        {
            _configuredBaseUrl = configuration["AppSettings:BaseUrl"]?.TrimEnd('/');
            _httpContextAccessor = httpContextAccessor;
        }

        public static string ToFullUrl(this string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return string.Empty;

            // Try to get the base URL from the current request first
            var baseUrl = _httpContextAccessor?.HttpContext?.Request?
                .GetTypedHeaders()?
                .Host.Value is string host
                ? $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}"
                : _configuredBaseUrl;

            if (string.IsNullOrWhiteSpace(baseUrl))
                return string.Empty;

            return $"{baseUrl}/{relativePath.TrimStart('/')}";
        }

        public static PhotoDto ToDto(this FileUpload entity)
        {
            return new PhotoDto
            {
                Id = entity.Id,
                PhotoUrl = entity.Path.ToFullUrl()
            };
        }
    }
}