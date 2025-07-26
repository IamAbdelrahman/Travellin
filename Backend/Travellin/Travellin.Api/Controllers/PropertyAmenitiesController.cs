using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.PropertyAmenities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Infrastructure.Shared;
using Travellin.Travellin.Core.Enums;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Api.Controllers
{
    [Authorize(Roles = "Admin,Host")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class PropertyAmenitiesController : BaseController
    {
        private readonly IAuthTokenService _authTokenService;
        public PropertyAmenitiesController(IUnitOfWork unitOfWork, IAuthTokenService authTokenService) : base(unitOfWork)
        {
            _authTokenService=authTokenService;

        }

        [HttpPost]
        [EndpointSummary("Create Property Amenity.")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PropertyAmenityDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] PropertyAmenityCreateDto dto)
        {
            await _authTokenService.EnsureEntityOwnershipAsync(dto.PropertyId, CurrentUser.Id, ErrorMessages.PropertyAmenitiesAdd, AuthRoles.Host | AuthRoles.Admin);

            if (await _unitOfWork.AmenityRepository.IsExistAsync(dto.PropertyId, dto.AmenityId))
            {
                throw new ConflictException("Amenity is already added to property.");
            }

            var newAmenity = dto.ToEntity();
            _unitOfWork.PropertyAmenityRepository.Create(newAmenity);
            await _unitOfWork.SaveChangesAsync();

            var amenity = await _unitOfWork.PropertyAmenityRepository.GetPropertyAmenityAsync(dto.PropertyId, dto.AmenityId);

            return new ObjectResult(amenity?.ToDto()) { StatusCode = 201 };
        }

        [HttpDelete("{propertyId}/{amenityId}")]
        [EndpointSummary("Delete existing Property Amenity.")]
        [ProducesResponseType(typeof(PropertyAmenityDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] string propertyId, int amenityId)
        {
            await _authTokenService.EnsureEntityOwnershipAsync(propertyId, CurrentUser.Id, ErrorMessages.PropertyAmenitiesDelete, AuthRoles.Host | AuthRoles.Admin);
            await _unitOfWork.PropertyAmenityRepository.DeleteAsync(propertyId, amenityId);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
