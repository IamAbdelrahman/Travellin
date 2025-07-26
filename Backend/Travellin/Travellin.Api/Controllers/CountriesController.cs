using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Countires;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using Travellin.Core.Mappings;

namespace Travellin.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountriesController : BaseController
    {
        public CountriesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
        [HttpGet]
        [EndpointSummary("Fetch all countries.")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginatedResult<CountryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllQueryDto queryDto)
        {
            var result = await _unitOfWork.CountryRepository.GetAllAsync(queryDto, q => q.OrderBy(x => x.Id));
            var resultDto = new PaginatedResult<CountryDto>
            {
                Items = result.Items.Select(x => x.ToDto()).ToList(),
                MetaData = result.MetaData
            };
            return Ok(resultDto);
        }
    }
}
