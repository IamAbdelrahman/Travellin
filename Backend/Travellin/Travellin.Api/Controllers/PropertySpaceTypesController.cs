using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.PropertySpaceTypes;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Api.Controllers
{
    public class PropertySpaceTypesController : BaseController
    {
        public PropertySpaceTypesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        [HttpGet]
        [EndpointSummary("Fetch all property space types.")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginatedResult<PropertySpaceTypeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllQueryDto queryDto)
        {
            var result = await _unitOfWork.PropertySpaceTypeRepository.GetAllAsync(queryDto, q => q.OrderBy(x => x.Id));

            var resultDto = new PaginatedResult<PropertySpaceTypeDto>
            {
                Items = result.Items.Select(x => x.ToDto()).ToList(),
                MetaData = result.MetaData
            };

            return Ok(resultDto);
        }
    }
}
