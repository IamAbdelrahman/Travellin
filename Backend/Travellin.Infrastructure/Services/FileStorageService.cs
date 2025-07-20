using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using System.Net;

namespace Travellin.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _webRootPath;
        private readonly string _imageDirectory;
        private readonly string[] _allowedExtensions;
        private readonly long _maxFileSizeBytes;

        public FileStorageService(IConfiguration config)
        {
            _webRootPath = config["FileStorage:WebRootPath"] ?? "wwwroot";
            _imageDirectory = config["FileStorage:ImageDirectory"] ?? "images";
            _allowedExtensions = config.GetSection("FileStorage:AllowedExtensions").Get<string[]>();
            _maxFileSizeBytes = (config.GetValue<int>("FileStorage:MaxFileSizeMB") * 1024 * 1024);
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            // Validation
            ValidateFile(file);

            // Create directory if not exists
            var uploadPath = Path.Combine(_webRootPath, _imageDirectory);
            Directory.CreateDirectory(uploadPath);

            // Generate unique filename
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path
            return Path.Combine(_imageDirectory, fileName).Replace('\\', '/');
        }

        public Task DeleteFileAsync(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return Task.CompletedTask;

            var fullPath = Path.Combine(_webRootPath, relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }

        private void ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ApiException("No file uploaded", HttpStatusCode.BadRequest);

            if (file.Length > _maxFileSizeBytes)
                throw new ApiException($"File exceeds maximum size of {_maxFileSizeBytes / (1024 * 1024)}MB", HttpStatusCode.BadRequest);

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                throw new ApiException($"Invalid file type. Allowed types: {string.Join(", ", _allowedExtensions)}", HttpStatusCode.BadRequest);
        }
    }
}