using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Amenities;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using Travellin.Core.Mappings;

namespace Travellin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : BaseController
    {
        public AmenitiesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        [HttpGet]
        [EndpointSummary("Fetch all aminities.")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginatedResult<AmenityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllQueryDto queryDto)
        {
            var result = await _unitOfWork.AmenityRepository.GetAllAsync(queryDto, q => q.OrderBy(x => x.Id));

            var resultDto = new PaginatedResult<AmenityDto>
            {
                Items = result.Items.Select(x => x.ToDto()).ToList(),
                MetaData = result.MetaData
            };

            return Ok(resultDto);
        }
    }
}
