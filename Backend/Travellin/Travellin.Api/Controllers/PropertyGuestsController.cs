using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos.PropertyGuests;
using Travellin.Infrastructure.Shared;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Api.Controllers
{
    [Authorize(Roles = "Host,Admin")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class PropertyGuestsController : BaseController
    {
        private readonly IAuthTokenService _authTokenService;
        public PropertyGuestsController(IUnitOfWork unitOfWork, IAuthTokenService authTokenService) : base(unitOfWork)
        {
            _authTokenService = authTokenService;
        }

        [HttpPost]
        [EndpointSummary("Create Property Guest.")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PropertyGuestDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] PropertyGuestCreateDto dto)
        {
            // Get property and verify ownership/access
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(dto.PropertyId);
            if (property == null)
            {
                return NotFoundResponse("Property not found");
            }

            await _authTokenService.EnsureEntityOwnershipAsync(property.Id, CurrentUser.Id, ErrorMessages.GuestCreate, AuthRoles.Host | AuthRoles.Admin);

            // Check if guest type already exists for this property
            var existingGuest = await _unitOfWork.PropertyGuestRepository
                .GetByPropertyAndGuestTypeAsync(dto.PropertyId, dto.GuestTypeId);

            if (existingGuest is not null)
            {
                return BadRequest(new List<string> {
                    "This property already has a configuration for this guest type"
                });
            }
            var newGuest = dto.ToEntity();

            _unitOfWork.PropertyGuestRepository.Create(newGuest);
            await _unitOfWork.SaveChangesAsync();

            var createdGuest = await _unitOfWork.PropertyGuestRepository
                .GetByPropertyAndGuestTypeAsync(dto.PropertyId, dto.GuestTypeId);

            return new ObjectResult(createdGuest?.ToDto()) { StatusCode = 201 };
        }

        [HttpPatch]
        [EndpointSummary("Update Property Guest.")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PropertyGuestDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] PropertGuestUpdateDto dto)
        {
            // Get property and verify ownership/access
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(dto.PropertyId);
            if (property == null)
            {
                return NotFoundResponse("Property not found");
            }

            await _authTokenService.EnsureEntityOwnershipAsync(property.Id, CurrentUser.Id, ErrorMessages.GuestUpdate, AuthRoles.Host | AuthRoles.Admin);

            // Get existing guest configuration
            var existingGuest = await _unitOfWork.PropertyGuestRepository
                .GetByPropertyAndGuestTypeAsync(dto.PropertyId, dto.GuestTypeId);

            if (existingGuest == null)
            {
                return NotFoundResponse("Guest configuration not found for this property");
            }

            existingGuest.GuestCount = dto.GuestCount;

            _unitOfWork.PropertyGuestRepository.Update(existingGuest);
            await _unitOfWork.SaveChangesAsync();

            return Ok(existingGuest.ToDto());
        }

        [HttpDelete("{propertyId}/{guestTypeId}")]
        [EndpointSummary("delete an existing Property Guest.")]
        public async Task<IActionResult> Delete([FromRoute] string propertyId, [FromRoute] int guestTypeId)
        {
            // Get property and verify ownership/access
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(propertyId);
            if (property == null)
            {
                return NotFoundResponse("Property not found");
            }
            await _authTokenService.EnsureEntityOwnershipAsync(property.Id, CurrentUser.Id, ErrorMessages.GuestDelete, AuthRoles.Host | AuthRoles.Admin);

            // Get existing guest configuration
            var existingGuest = await _unitOfWork.PropertyGuestRepository
                .GetByPropertyAndGuestTypeAsync(propertyId, guestTypeId);

            if (existingGuest == null)
            {
                return NotFoundResponse("Guest configuration not found for this property");
            }

            _unitOfWork.PropertyGuestRepository.Delete(existingGuest);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
