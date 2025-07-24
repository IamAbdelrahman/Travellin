using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos.HostUpgradeRequests;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using Travellin.Core.Mappings;
using Microsoft.AspNetCore.Authorization;
using Travellin.Core.Enums;
using Travellin.Core.Entities;


namespace Travellin.Api.Controllers
{
    public class HostUpgradeRequestsController : BaseController
    {
        private IServiceFactory _serviceFactory;
        private IIdentityFactory _identityFactory;
        public HostUpgradeRequestsController(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, IIdentityFactory identityFactory) : base(unitOfWork)
        {
            _serviceFactory = serviceFactory;
            _identityFactory = identityFactory;
        }

        [HttpPost]
        [Authorize(Roles = "Guest")]
        [EndpointSummary("Create new Host upgrade request.")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(HostUpgradeRequestDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromForm] HostUpgradeRequestCreateDto reqDto)
        {
            var userId = CurrentUser.Id;

            if (CurrentUser.Roles.Contains("Host"))
            {
                return BadRequest(new List<string> { "You are already a Host. Upgrade request is not allowed." });
            }

            var existingRequest = await _unitOfWork.HostUpgradeRequestRepository.GetPendingRequestByUserIdAsync(userId);

            if (existingRequest != null)
            {
                return BadRequest(new List<string> { "You already have a pending host upgrade request." });
            }

            var frontPhoto = await _serviceFactory.FileUploadManagementService.UploadAsync(reqDto.FrontPhoto);
            var backPhoto = await _serviceFactory.FileUploadManagementService.UploadAsync(reqDto.BackPhoto);

            var documentType = Enum.Parse<HostUpgradeRequestDocumentType>(reqDto.DocumentType, ignoreCase: true);

            var hostUpgradeRequest = new HostUpgradeRequest
            {
                UserId = userId,
                Status = HostUgradeRequestStatus.Pending,
                DocumentType = documentType,
                DocumentNumber = reqDto.DocumentNumber,
                FrontPhotoId = frontPhoto.Id,
                BackPhotoId = backPhoto.Id
            };

            _unitOfWork.HostUpgradeRequestRepository.Create(hostUpgradeRequest);
            await _unitOfWork.SaveChangesAsync();
            var createdRequest = await _unitOfWork.HostUpgradeRequestRepository.GetByIdAsync(hostUpgradeRequest.Id, x => x.FrontPhoto, x => x.BackPhoto);
            return new ObjectResult(createdRequest.ToDto()) { StatusCode = 201 };
        }

        [HttpPatch("my-request")]
        [Authorize(Roles = "Guest")]
        [EndpointSummary("Allow users to update their own Host upgrade request.")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(HostUpgradeRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMyRequest([FromForm] HostUpgradeRequestUpdateDto updateDto)
        {
            var userId = CurrentUser.Id;

            var existingRequest = await _unitOfWork.HostUpgradeRequestRepository
                .GetPendingRequestByUserIdAsync(userId);

            if (existingRequest == null)
            {
                return BadRequest(new List<string> { "You do not have a pending request to update." });
            }

            if (!string.IsNullOrWhiteSpace(updateDto.DocumentType))
            {
                var parsed = Enum.Parse<HostUpgradeRequestDocumentType>(updateDto.DocumentType, ignoreCase: true);
                existingRequest.DocumentType = parsed;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.DocumentNumber))
            {
                existingRequest.DocumentNumber = updateDto.DocumentNumber;
            }

            if (updateDto.FrontPhoto != null)
            {
                var uploadedFront = await _serviceFactory.FileUploadManagementService.UploadAsync(updateDto.FrontPhoto);
                await _serviceFactory.FileUploadManagementService.RemoveFileAsync(existingRequest.FrontPhotoId);
                existingRequest.FrontPhotoId = uploadedFront.Id;
            }

            if (updateDto.BackPhoto != null)
            {
                var uploadedBack = await _serviceFactory.FileUploadManagementService.UploadAsync(updateDto.BackPhoto);
                await _serviceFactory.FileUploadManagementService.RemoveFileAsync(existingRequest.BackPhotoId);
                existingRequest.BackPhotoId = uploadedBack.Id;
            }

            _unitOfWork.HostUpgradeRequestRepository.Update(existingRequest);
            await _unitOfWork.SaveChangesAsync();

            var updated = await _unitOfWork.HostUpgradeRequestRepository
                .GetByIdAsync(existingRequest.Id, x => x.FrontPhoto, x => x.BackPhoto);

            return Ok(updated.ToDto());
        }

        [HttpGet("my-request")]
        [Authorize(Roles = "Guest")]
        [EndpointSummary("Allow users to fetch their own Host upgrade request.")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(HostUpgradeRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMyRequest()
        {
            var userId = CurrentUser.Id;

            var exitingRequest = await _unitOfWork.HostUpgradeRequestRepository.GetLastRequestByUserIdASync(userId);

            if (exitingRequest is null)
            {
                return NotFoundResponse("You do not have any upgrade requests.");
            }

            return Ok(exitingRequest.ToDto());
        }

        [HttpPatch("{requestId}/approve")]
        [Authorize(Roles = "Admin")]
        [EndpointSummary("Allow admins to approve Host upgrade request.")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(HostUpgradeRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApproveRequest([FromRoute] string requestId)
        {
            var adminId = CurrentUser.Id;
            var request = await _unitOfWork.HostUpgradeRequestRepository.GetByIdAsync(requestId);

            if (request is null)
                return NotFoundResponse();

            var user = await _identityFactory.UserManager.FindByIdAsync(request.UserId);
            var roleResult = await _identityFactory.UserManager.AddToRoleAsync(user, "Host");
            if (!roleResult.Succeeded)
            {
                throw new BadHttpRequestException($"Failed to add user to Host role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }

            request.ApprovedBy = adminId;
            request.Status = HostUgradeRequestStatus.Approved;
            request.ApprovalDate = DateTime.UtcNow;

            _unitOfWork.HostUpgradeRequestRepository.Update(request);
            await _unitOfWork.SaveChangesAsync();

            var updatesRequest = await _unitOfWork.HostUpgradeRequestRepository.GetByIdAsync(requestId, x => x.FrontPhoto, x => x.BackPhoto);

            return Ok(updatesRequest.ToDto());
        }

        [HttpPatch("{requestId}/reject")]
        [Authorize(Roles = "Admin")]
        [EndpointSummary("Allow admins to reject Host upgrade request.")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(HostUpgradeRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RejectRequest(string requestId, [FromBody] HostUpgradeRequestRejectDto reqDto)
        {
            var adminId = CurrentUser.Id;
            var request = await _unitOfWork.HostUpgradeRequestRepository.GetByIdAsync(requestId);

            if (request is null)
                return NotFoundResponse();

            request.ApprovedBy = adminId;
            request.Status = HostUgradeRequestStatus.Rejected;
            request.RejectionReason = reqDto.RejectionReason;
            request.ApprovalDate = DateTime.UtcNow;

            _unitOfWork.HostUpgradeRequestRepository.Update(request);
            await _unitOfWork.SaveChangesAsync();

            var updatesRequest = await _unitOfWork.HostUpgradeRequestRepository.GetByIdAsync(requestId, x => x.FrontPhoto, x => x.BackPhoto);

            return Ok(updatesRequest.ToDto());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [EndpointSummary("Allow admins to fetch Host upgrade requests.")]
        [EndpointDescription("Sorting options for the 'sort' query parameter: \n" +
            "• `status` – Sort by request status ascending\n" +
            "• `created_at_asc` – Sort by creation date ascending\n" +
            "• `created_at_desc` – Sort by creation date descending\n" +
            "• `approval_date_asc` – Sort by approval date ascending\n" +
            "• `approval_date_desc` – Sort by approval date descending")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginatedResult<HostUpgradeRequestDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequests([FromQuery] HostUpgradeRequestFilterQueryParamsDto queryDto)
        {
            var result = await _unitOfWork.HostUpgradeRequestRepository.GetAllAsync(queryDto);

            return Ok(result);
        }
    }
}
