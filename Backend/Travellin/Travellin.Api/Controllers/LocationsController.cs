using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using Travellin.Core.Mappings;
using Travellin.Core.Dtos.Locations;

namespace Travellin.Api.Controllers
{
    public class LocationsController : BaseController
    {
        public LocationsController(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        [HttpGet]
        [EndpointSummary("Fetch all locations.")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginatedResult<LocationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] LocationQueryParamsDto queryDto)
        {
            var result = await _unitOfWork.LocationRepository.GetFilteredLocationsAsync(queryDto);
            return Ok(result);
        }
    }
}
