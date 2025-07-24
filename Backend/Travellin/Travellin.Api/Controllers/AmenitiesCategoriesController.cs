using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using Travellin.Core.Mappings;
using Travellin.Core.Dtos.AmenityCategories;

namespace Travellin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesCategoriesController : BaseController
    {
        public AmenitiesCategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
        [HttpGet]
        [EndpointSummary("Fetch all amenity categories.")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginatedResult<AmenityCategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllQueryDto queryDto)
        {
            var result = await _unitOfWork.AmenityCategoryRepository.GetAllAsync(queryDto, q => q.OrderBy(x => x.Id));
            var resultDto = new PaginatedResult<AmenityCategoryDto>
            {
                Items = result.Items.Select(x => x.ToDto()).ToList(),
                MetaData = result.MetaData
            };
            return Ok(resultDto);
        }
    }
}
