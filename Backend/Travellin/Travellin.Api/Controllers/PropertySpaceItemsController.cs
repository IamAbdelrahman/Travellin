using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Travellin.Core.Dtos.PropertySpaceItems;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Infrastructure.Shared;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Api.Controllers
{
    [Authorize(Roles = "Admin,Host")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class PropertySpaceItemsController : BaseController
    {
        private readonly IAuthTokenService _authTokenService;
        public PropertySpaceItemsController(IUnitOfWork unitOfWork, IAuthTokenService authTokenService) : base(unitOfWork)
        {
            _authTokenService=authTokenService;
        }

        [Authorize(Roles = "Admin,Host")]
        [HttpPost]
        [EndpointSummary("Create Property Space Itme.")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PropertySpaceItemDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] PropertySpaceItemCreateDto dto)
        {
            if (!CurrentUser.IsInRole("Admin") && !CurrentUser.IsInRole("Host"))
                return BadRequest("You don't have permission");
            var newSpaceItem = dto.ToEntity();
            _unitOfWork.PropertySpaceItemRepository.Create(newSpaceItem);
            await _unitOfWork.SaveChangesAsync();
            return new ObjectResult(newSpaceItem.ToDto()) { StatusCode = 201 };
        }

        [Authorize(Roles = "Admin,Host")]
        [HttpPatch("{id}")]
        [EndpointSummary("Update exiting Property Space Itme by id.")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PropertySpaceItemDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PropertySpaceItemUpdateDto dto)
        {
            if (!CurrentUser.IsInRole("Admin") && !CurrentUser.IsInRole("Host"))
                return BadRequest("You don't have permission");
            var exitingItem = await _unitOfWork.PropertySpaceItemRepository.GetByIdAsync(id);

            if (exitingItem is null)
            {
                return NotFoundResponse();
            }
            exitingItem.PropertySpaceItemTypeId = dto.PropertySpaceItemTypeId.HasValue ? dto.PropertySpaceItemTypeId.Value : exitingItem.PropertySpaceItemTypeId;
            exitingItem.Quantity = dto.Quantity.HasValue ? dto.Quantity.Value : exitingItem.Quantity;


            _unitOfWork.PropertySpaceItemRepository.Update(exitingItem);
            await _unitOfWork.SaveChangesAsync();

            return Ok(exitingItem.ToDto());
        }

        [Authorize(Roles = "Admin,Host")]
        [HttpDelete("{id}")]
        [EndpointSummary("Update exiting Property Space Itme by id.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!CurrentUser.IsInRole("Admin") && !CurrentUser.IsInRole("Host"))
                return BadRequest("You don't have permission");
            var item = await _unitOfWork.PropertySpaceItemRepository.GetByIdAsync(id);

            if (item is null)
            {
                return NotFoundResponse();
            }

            _unitOfWork.PropertySpaceItemRepository.Delete(item);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
