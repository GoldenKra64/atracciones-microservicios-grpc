using Asp.Versioning;
using Atraccion.Microservicios.Atraccion.Api.Models.Common;
using Atraccion.Microservicios.Atraccion.Business.DTOs.NoIncluye;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atraccion.Microservicios.Atraccion.Api.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v2/[controller]")]
    public class NoIncluyeController : ControllerBase
    {
        private readonly INoIncluyeBusinessService _service;

        public NoIncluyeController(INoIncluyeBusinessService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<NoIncluyeResponse>>.Ok(data));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<NoIncluyeResponse>.Ok(data));
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create(CreateNoIncluyeRequest request)
        {
            var id = await _service.CreateAsync(request);
            return Ok(ApiResponse<int>.Ok(id, "No Incluye creado"));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(UpdateNoIncluyeRequest request, int id)
        {
            request.Id = id;
            await _service.UpdateAsync(request);
            return Ok(ApiResponse<string>.Ok("OK", "No Incluye actualizado"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.LogicalDeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("OK", "No Incluye eliminado"));
        }
    }
}
