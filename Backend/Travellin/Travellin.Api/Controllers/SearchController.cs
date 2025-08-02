using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Dtos.Properties;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Shared;

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
        [HttpPost("smartSearch")]
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
            foreach (var property in propertiesResult.Items)
            {
                _unitOfWork.RecommendationRepository.Create(new Recommendations
                {
                    UserId = CurrentUser.Id,   
                    Query = query,
                    PropertyId = property.Id,
                    Score = 1.0
                });
            }
            await _unitOfWork.SaveChangesAsync();

            var result = new PropertySearchResultDto
            {
                Items = propertiesResult.Items,
                MetaData = propertiesResult.MetaData,
                SearchParams = dto,
            };

            return Ok(result);
        }

        [HttpGet("recommendations")]
        public async Task<IActionResult> GetRecommendations()
        {
            var recommendations = await _unitOfWork.RecommendationRepository
                .GetUserRecommendationsAsync(CurrentUser.Id);


            var propertyIds = recommendations.Select(r => r.PropertyId).Distinct();
            List<PropertyDetailsDto> properties = new List<PropertyDetailsDto>();
            foreach (var id in propertyIds)
            {
                var p = await _unitOfWork.PropertyRepository.GetPropertyDetailsAsync(id, CurrentUser);
                properties.Add(p);
            }

            return Ok(properties);
        }

    }

}


