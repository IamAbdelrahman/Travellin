using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.PropertySpaceItems;
using Travellin.Core.Dtos.PropertySpaces;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Infrastructure.Shared;
using Travellin.Travellin.Core.Enums;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Api.Controllers
{
    public class PropertySpacesController : BaseController
    {
        private readonly IAuthTokenService _authTokenService;
        public PropertySpacesController(IUnitOfWork unitOfWork, IAuthTokenService authTokenService) : base(unitOfWork)
        {
            _authTokenService = authTokenService;
        }

        [HttpGet("{id}/Items")]
        [EndpointSummary("Fetch property spaces items by id.")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginatedResult<PropertySpaceItemDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(List<string>))]
        public async Task<IActionResult> GetItemsBySpaceId([FromRoute] string id, [FromQuery] GetAllQueryDto dto)
        {
            var result = await _unitOfWork.PropertySpaceItemRepository.GetByPropertySpaceIdAsync(id, dto);

            return Ok(result);
        }

        [Authorize(Roles = "Admin,Host")]
        [HttpPost]
        [EndpointSummary("Create Property Space.")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PropertySpaceDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] PropertySpaceCreateDto dto)
        {
            await _authTokenService.EnsureEntityOwnershipAsync(dto.PropertyId, CurrentUser.Id, ErrorMessages.PropertySpaceAdd, AuthRoles.Host | AuthRoles.Admin);

            var newSpace = new PropertySpace
            {
                PropertyId = dto.PropertyId,
                Name = dto.Name,
                PropertySpaceTypeId = dto.PropertySpaceTypeId,
                IsShared = dto.IsShared
            };

            _unitOfWork.PropertySpaceRepository.Create(newSpace);
            await _unitOfWork.SaveChangesAsync();
            return new ObjectResult(newSpace.ToDto()) { StatusCode = 201 };
        }

        [Authorize(Roles = "Admin,Host")]
        [HttpPatch("{id}")]
        [EndpointSummary("Update exiting Property Space.")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PropertySpaceDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] PropertySpaceUpdateDto dto)
        {
            var existingSpace = await _unitOfWork.PropertySpaceRepository.GetByIdAsync(id);

            if (existingSpace is null)
            {
                return NotFoundResponse();
            }

            await _authTokenService.EnsureEntityOwnershipAsync(id, CurrentUser.Id, ErrorMessages.PropertySpaceUpdate, AuthRoles.Host | AuthRoles.Admin);

            existingSpace.Name = !string.IsNullOrEmpty(dto.Name) ? dto.Name : existingSpace.Name;
            existingSpace.PropertySpaceTypeId = dto.PropertySpaceTypeId.HasValue ? dto.PropertySpaceTypeId.Value : existingSpace.PropertySpaceTypeId;
            existingSpace.IsShared = dto.IsShared.HasValue ? dto.IsShared.Value : existingSpace.IsShared;

            _unitOfWork.PropertySpaceRepository.Update(existingSpace);
            await _unitOfWork.SaveChangesAsync();

            return Ok(existingSpace.ToDto());
        }

        [Authorize(Roles = "Admin,Host")]
        [HttpDelete("{id}")]
        [EndpointSummary("Delete Property Space.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var existingSpace = await _unitOfWork.PropertySpaceRepository.GetByIdAsync(id);

            if (existingSpace is null)
            {
                return NotFoundResponse();
            }

            await _authTokenService.EnsureEntityOwnershipAsync(id, CurrentUser.Id, ErrorMessages.PropertySpaceDelete, AuthRoles.Host | AuthRoles.Admin);

            _unitOfWork.PropertySpaceRepository.Delete(existingSpace);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
