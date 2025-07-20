using Microsoft.AspNetCore.Http;

namespace Travellin.Core.Interfaces
{
    public interface IFileStorageService
    {
        public Task<string> SaveFileAsync(IFormFile file);
        public Task DeleteFileAsync(string path);
    }
}
