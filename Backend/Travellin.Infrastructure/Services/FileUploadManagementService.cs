using Microsoft.AspNetCore.Http;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;

namespace Travellin.Infrastructure.Services
{
    class FileUploadManagementService : IFileUploadManagementService
    {
        private IUnitOfWork _unitOfWork;
        private IFileStorageService _fileStorageService;
        public FileUploadManagementService(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }


        public async Task<FileUpload> UploadAsync(IFormFile file)
        {
            var path = await _fileStorageService.SaveFileAsync(file);
            var newFileUpload = new FileUpload
            {
                Path = path,
            };

            _unitOfWork.FileUploadRepository.Create(newFileUpload);
            await _unitOfWork.FileUploadRepository.SaveChangesAsync();
            return newFileUpload;
        }

        public async Task RemoveFileAsync(string fileUploadId)
        {
            var fileUplaod = await _unitOfWork.FileUploadRepository.GetByIdAsync(fileUploadId);

            if (fileUplaod == null) return;

            await _fileStorageService.DeleteFileAsync(fileUplaod.Path);
            _unitOfWork.FileUploadRepository.Delete(fileUplaod);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
