using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos.Properties;
using Travellin.Core.Interfaces;

namespace Travellin.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SearchController : BaseController
    {
        private readonly IServiceFactory _serviceFactory;
        public SearchController(IUnitOfWork unitOfWork, IServiceFactory serviceFactory) : base(unitOfWork)
        {
            _serviceFactory = serviceFactory;
        }
        [HttpPost("search")]
        [EndpointSummary("Smart search for properity.")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PropertySearchResultDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(List<string>))]
        public async Task<IActionResult> Search([FromBody] string query)
        {
            var dto = await _serviceFactory.PropertyFilterExtractorService.ExtractFiltersAsync(query);

            dto.PropertyTypeId = null;

            var propertiesResult = await _unitOfWork.PropertyRepository.GetFilteredPropertiesAsync(dto, CurrentUser);

            var result = new PropertySearchResultDto
            {
                Items = propertiesResult.Items,
                MetaData = propertiesResult.MetaData,
                SearchParams = dto,
            };

            return Ok(result);
        }
    }
}
