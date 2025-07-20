using Microsoft.AspNetCore.Http;
using Travellin.Core.Entities;

namespace Travellin.Core.Interfaces
{
    public interface IFileUploadManagementService
    {
        public Task<FileUpload> UploadAsync(IFormFile file);
        public Task RemoveFileAsync(string fileUploadId);
    }
}
