using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos.PropertyAvailabilities;
using Travellin.Core.Dtos.PropertyGuests;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Infrastructure.Services;
using Travellin.Infrastructure.Shared;
using Travellin.Travellin.Core.Enums;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Api.Controllers
{
    [Authorize(Roles = "Admin,Host")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class PropertyAvailabilitiesController : BaseController
    {
        private readonly AuthTokenService _authTokenService;
        public PropertyAvailabilitiesController(IUnitOfWork unitOfWork, AuthTokenService authTokenService) : base(unitOfWork)
        {
            _authTokenService = authTokenService;
        }

        [HttpPost]
        [EndpointSummary("Create Property Availability.")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PropertyGuestDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] PropertyAvailabilityCreateDto dto)
        {
            await _authTokenService.EnsureEntityOwnershipAsync(dto.PropertyId, CurrentUser.Id, ErrorMessages.PropertyAvailabilityAdd, AuthRoles.Host | AuthRoles.Admin);
            var newPropertyAvailability = dto.ToEntity();

            _unitOfWork.PropertyAvailabilityRepository.Create(newPropertyAvailability);
            await _unitOfWork.SaveChangesAsync();
            return new ObjectResult(newPropertyAvailability.ToDto()) { StatusCode = 201 };
        }

        [HttpPatch("{id}")]
        [EndpointSummary("Update Property Availability.")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PropertyGuestDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PropertyAvailabilityUpdateDto dto)
        {
            var existingAvailability = await _unitOfWork.PropertyAvailabilityRepository.GetByIdAsync(id);

            if (existingAvailability is null)
            {
                return NotFoundResponse();
            }

            await _authTokenService.EnsureEntityOwnershipAsync(existingAvailability.PropertyId, CurrentUser.Id, ErrorMessages.PropertyAvailabilityUpdate, AuthRoles.Host | AuthRoles.Admin);

            existingAvailability.StartDate = dto.StartDate.HasValue ? dto.StartDate.Value : existingAvailability.StartDate;
            existingAvailability.EndDate = dto.EndDate.HasValue ? dto.EndDate.Value : existingAvailability.EndDate;

            _unitOfWork.PropertyAvailabilityRepository.Update(existingAvailability);
            await _unitOfWork.SaveChangesAsync();
            return Ok(existingAvailability.ToDto());
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Delete existing Property Availability.")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PropertyGuestDto), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var existingAvailability = await _unitOfWork.PropertyAvailabilityRepository.GetByIdAsync(id);

            if (existingAvailability is null)
            {
                return NotFoundResponse();
            }
            await _authTokenService.EnsureEntityOwnershipAsync(existingAvailability.PropertyId, CurrentUser.Id, ErrorMessages.PropertyAvailabilityDelete, AuthRoles.Host | AuthRoles.Admin);
            _unitOfWork.PropertyAvailabilityRepository.Delete(existingAvailability);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
