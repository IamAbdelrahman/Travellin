using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travellin.Api.Controllers;
using Travellin.Core.Dtos.UserProfilesDto;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using Travellin.Core.Mappings;
namespace Travellin.Travellin.Api.Controllers
{
    [Authorize]
    public class UserProfilesController : BaseController
    {
        private readonly IServiceFactory _serviceFactory;
        public UserProfilesController(IUnitOfWork unitOfWork, IServiceFactory serviceFactory) : base(unitOfWork)
        {
            _serviceFactory = serviceFactory;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [EndpointSummary("Get all user profiles (For Admins only).")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginatedResult<UserProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] UserProfileQueryParamsDto dto)
        {
            // Ensure the user has admin role
            if (!CurrentUser.IsInRole("Admin"))
            {
                return Unauthorized(new { Message = "You do not have permission to access this resource." });
            }
            var result = await _unitOfWork.UserProfileRepository.GetFilteredProfilesAsync(dto);

            return Ok(result);
        }

        [HttpGet("me")]
        [EndpointSummary("Get my profile Info.")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByUserId()
        {
            var userId = CurrentUser.Id;
            var userProfile = await _unitOfWork.UserProfileRepository.GetProfileDetailsByUserId(userId);

            return Ok(userProfile);

        }

        [HttpPatch("me")]
        [EndpointSummary("Update my profile Info.")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateByUserId([FromForm] UpdateUserProfileDto dto)
        {
            var userId = CurrentUser.Id;
            var userProfile = await _unitOfWork.UserProfileRepository.GetByUserId(userId);

            // Handle photo upload if provided
            if (dto.Photo != null)
            {
                var oldPhotoId = userProfile.PhotoId;
                var fileUpload = await _serviceFactory.FileUploadManagementService.UploadAsync(dto.Photo);
                userProfile.PhotoId = fileUpload.Id;

                // Delete old photo after successful upload
                if (!string.IsNullOrEmpty(oldPhotoId))
                {
                    await _serviceFactory.FileUploadManagementService.RemoveFileAsync(oldPhotoId);
                }
            }

            // Update other properties
            dto.ToEntity(userProfile);

            // Save changes
            _unitOfWork.UserProfileRepository.Update(userProfile);
            await _unitOfWork.SaveChangesAsync();

            // Return updated profile details
            var updatedProfile = await _unitOfWork.UserProfileRepository.GetProfileDetailsByUserId(userId);
            return Ok(updatedProfile);
        }
    }
}
