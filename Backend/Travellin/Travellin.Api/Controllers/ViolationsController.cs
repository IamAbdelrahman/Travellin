using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Travellin.Core.Dtos.Violations;
using Travellin.Core.Interfaces;

namespace Travellin.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ViolationsController : ControllerBase
    {
        private readonly IViolationService _violationService;

        public ViolationsController(IViolationService violationService)
        {
            _violationService = violationService;
        }

        [HttpPost]
        public async Task<IActionResult> ReportViolation([FromBody] CreateViolationDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _violationService.CreateAsync(dto, userId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] string status = "Pending")
        {
            var violations = await _violationService.GetAllAsync(status);
            return Ok(violations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var violation = await _violationService.GetByIdAsync(id);
            return violation != null ? Ok(violation) : NotFound();
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateViolationStatusDto dto)
        {
            var result = await _violationService.UpdateStatusAsync(id, dto);
            return result ? Ok() : BadRequest();
        }

        [HttpGet("my-reports")]
        public async Task<IActionResult> GetMyReports()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var violations = await _violationService.GetByReporterAsync(userId);
            return Ok(violations);
        }
    }
}