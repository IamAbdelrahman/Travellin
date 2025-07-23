using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travellin.Core.Interfaces;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Properties;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        [HttpPut("api/update/{id}")]
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
            //_unitOfWork.SaveChangesAsync();
            _unitOfWork.SaveChanges();
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var properties =await _unitOfWork.PropertyRepository.GetAll();
            return Ok(properties);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(id);
            if (property == null)
            {
                return NotFound($"No property found with ID : {id}");
            }
            if (id == property.Id)
            await _unitOfWork.PropertyRepository.DeleteAsync(property);
            await _unitOfWork.SaveChangesAsync();
            return Ok($"Propery with ID : {id} is DELETED successfully" );

        }

        //[HttpPost("addProperty")]
        //public async Task<IActionResult> Create([FromBody] PropertyCreateDto dto)
        //{
        //    var created =await _unitOfWork.PropertyRepository.Create(dto);
        //    return Ok(created);

        //}




    }
}
