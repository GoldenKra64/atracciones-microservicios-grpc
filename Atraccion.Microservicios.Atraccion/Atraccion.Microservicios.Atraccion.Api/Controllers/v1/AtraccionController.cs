using Asp.Versioning;
using Atraccion.Microservicios.Atraccion.Api.Models.Common;
using Atraccion.Microservicios.Atraccion.Business.DTOs;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atraccion.Microservicios.Atraccion.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/atracciones")]
    public class AtraccionController : ControllerBase
    {
        private readonly IAtraccionBusinessService _service;

        public AtraccionController(IAtraccionBusinessService service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<AtraccionDetalleDto>.Ok(data, "Detalle de atracciones obtenido exitosamente"));
        }

        [HttpGet("type")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetType()
        {
            var data = await _service.GetAtraccionType();
            return Ok(ApiResponse<List<AtraccionTypeResponse>>.Ok(data, "Tipos de atracciones obtenidos exitosamente"));
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] FiltroDto? filtro)
        {
            var data = await _service.GetPagedAsync(filtro);
            return Ok(ApiResponse<PagedResponse<ListadoAtracciones>>.Ok(data, "Listado de atracciones obtenido exitosamente"));
        }

        [HttpGet("filtros")]
        public async Task<IActionResult> GetFiltros()
        {
            var data = await _service.GetFiltrosAsync();
            return Ok(ApiResponse<FiltrosDisponiblesDto>.Ok(data, "Filtros disponibles obtenidos exitosamente"));
        }

        [HttpGet("internal/{id:guid}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetByIdInternal(string id)
        {
            var data = await _service.GetInternalById(id);
            return Ok(ApiResponse<AtraccionResponse>.Ok(data, "Atracción obtenida exitosamente"));
        }


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create(CreateAtraccionRequest request)
        {
            await _service.CreateAsync(request);
            return Created();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(UpdateAtraccionRequest request, string id)
        {
            request.Id = id;
            await _service.UpdateAsync(request);
            return Ok(ApiResponse<string>.Ok("OK"));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.LogicalDeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("OK"));
        }

        // Contratos
        [HttpGet("{guid:guid}/tickets")]
        public async Task<IActionResult> GetTicketsForAttraction(string guid)
        {
            var data = await _service.GetTicketsByAttraction(guid);
            return Ok(ApiResponse<List<TicketDto>>.Ok(data, "Listado de tickets para la atracción"));
        }

        [HttpGet("{guid:guid}/horarios-disponibles")]
        public async Task<IActionResult> GetHorariosProximosByAttraction(string guid)
        {
            var data = await _service.GetHorariosByAttraction(guid);
            return Ok(ApiResponse<List<HorarioDto>>.Ok(data, "Listado de cupos con horarios por atracción"));
        }
    }
}
