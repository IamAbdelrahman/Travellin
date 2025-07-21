using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Interfaces;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Properties;
namespace Travellin.Travellin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceFactory _serviceFactory;
        public PropertiesController(IUnitOfWork unitOfWork, IServiceFactory serviceFactory)
        {
            _unitOfWork = unitOfWork;
            _serviceFactory=serviceFactory;
        }
        [HttpPost("api/update/{id}")]
        //[Authorize(Roles = "Host")]

        public IActionResult UpdateProperty([FromBody]PropertyUpdateDto dto, string id)
        {
            //var property = await _unitOfWork.PropertyRepository.GetByIdAsync(id);
            var property = _unitOfWork.PropertyRepository.GetById(id);
            if (property == null)
            {
                return BadRequest($"There is no property with this {id}");
            }
            _unitOfWork.PropertyRepository.FromUpdateDtoToEntity(property, dto);
            _unitOfWork.PropertyRepository.Update(property);
            _unitOfWork.SaveChangesAsync();
            //_unitOfWork.SaveChanges();
            return Ok(property);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            //var result = await _unitOfWork.PropertyRepository.GetByIdAsync(id);
            var result = _unitOfWork.PropertyRepository.GetById(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
