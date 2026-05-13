using Asp.Versioning;
using Atraccion.Microservicios.Atraccion.Api.Models.Common;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atraccion.Microservicios.Atraccion.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioBusinessService _service;

        public HorarioController(IHorarioBusinessService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<HorarioDto>>.Ok(data));
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<HorarioDto>.Ok(data));
        }


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create(CreateHorarioRequest request)
        {
            var id = await _service.CreateAsync(request);
            return Ok(ApiResponse<int>.Ok(id, "Horario creado"));
        }

        [HttpPut("{guid:guid}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(UpdateHorarioRequest request, string guid)
        {
            request.Guid = guid;
            await _service.UpdateAsync(request);
            return Ok(ApiResponse<string>.Ok("OK", "Horario actualizado"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.LogicalDeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("OK", "Horario eliminado"));
        }
    }
}
