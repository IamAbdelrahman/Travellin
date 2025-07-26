using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Travellin.Core.Dtos.PropertyPhotos;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Infrastructure.Shared;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Api.Controllers
{
    [Authorize(Roles = "Host,Admin")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class PropertyPhotosController : BaseController
    {
        private IServiceFactory _serviceFactory;
        private readonly IAuthTokenService _authTokenService;
        public PropertyPhotosController(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, IAuthTokenService authTokenService) : base(unitOfWork)
        {
            _serviceFactory = serviceFactory;
            _authTokenService = authTokenService;

        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [EndpointSummary("Upload property photos.")]
        [ProducesResponseType(typeof(List<PropertyPhotoDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Upload([FromForm] PropertyPhotosUploadDto dto)
        {
            var userId = CurrentUser.Id;
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(dto.PropertyId);

            if (property is null)
            {
                return NotFoundResponse("Property not found");
            }

            await _authTokenService.EnsureEntityOwnershipAsync(property.Id, userId, ErrorMessages.PhotoUpload, AuthRoles.Host | AuthRoles.Admin);

            for (int i = 0; i < dto.Photos.Count; ++i)
            {
                var fileUpload = await _serviceFactory.FileUploadManagementService.UploadAsync(dto.Photos[i]);
                _unitOfWork.PropertyPhotoRepository.Create(new PropertyPhoto
                {
                    PhotoId = fileUpload.Id,
                    PropertyId = property.Id,
                    TouchedAt = DateTime.UtcNow.AddSeconds(i)
                });
            }

            await _unitOfWork.SaveChangesAsync();

            var propertyPhotos = await _unitOfWork.PropertyPhotoRepository.GetAllByPropertyIdASync(property.Id);

            return Ok(propertyPhotos.Select(x => x.ToDto()).ToList());
        }

        [HttpPost("reorder")]
        [EndpointSummary("Reoder property photos.")]
        [ProducesResponseType(typeof(List<PropertyPhotoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Reorder([FromBody] PropertyPhotosReorderDto dto)
        {
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(dto.PropertyId);

            if (property is null)
            {
                return NotFoundResponse("Property not found");
            }

            await _authTokenService.EnsureEntityOwnershipAsync(property.Id, CurrentUser.Id, ErrorMessages.PhotoReorder, AuthRoles.Host | AuthRoles.Admin);


            var existingPhotos = await _unitOfWork.PropertyPhotoRepository.GetAllByPropertyIdASync(property.Id);

            if (!existingPhotos.Any())
            {
                return NotFoundResponse("No photos found for this property");
            }

            // Validate all photo IDs in request exist for this property
            var existingPhotoIds = existingPhotos.Select(p => p.PhotoId).ToList();
            var invalidPhotoIds = dto.PhotoIds.Except(existingPhotoIds).ToList();
            if (invalidPhotoIds.Any())
            {
                return BadRequest(new List<string> {
            $"The following photo IDs don't belong to this property: {string.Join(", ", invalidPhotoIds)}"
        });
            }

            // Check if we have all photos in the request
            var missingPhotoIds = existingPhotoIds.Except(dto.PhotoIds).ToList();
            if (missingPhotoIds.Any())
            {
                return BadRequest(new List<string> {
            $"The following photos are missing from the reorder request: {string.Join(", ", missingPhotoIds)}"
        });
            }

            var photoDict = existingPhotos.ToDictionary(p => p.PhotoId);

            var now = DateTime.UtcNow;
            for (int i = 0; i < dto.PhotoIds.Count; i++)
            {
                var photo = photoDict[dto.PhotoIds[i]];
                photo.TouchedAt = now.AddSeconds(i);
                _unitOfWork.PropertyPhotoRepository.Update(photo);
            }

            await _unitOfWork.SaveChangesAsync();

            var propertyPhotos = await _unitOfWork.PropertyPhotoRepository.GetAllByPropertyIdASync(property.Id);

            return Ok(propertyPhotos.Select(x => x.ToDto()).ToList());
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Delete property photos.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _authTokenService.EnsureEntityOwnershipAsync(id, CurrentUser.Id, ErrorMessages.PhotoDelete, AuthRoles.Host | AuthRoles.Admin);
            await _serviceFactory.FileUploadManagementService.RemoveFileAsync(id);
            return NoContent();
        }
    }
}
