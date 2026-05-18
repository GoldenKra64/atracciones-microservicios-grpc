using Asp.Versioning;
using Atraccion.Microservicios.Atraccion.Api.Models.Common;
using Atraccion.Microservicios.Atraccion.Business.DTOs;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Resena;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using Atraccion.Microservicios.Atraccion.Business.Exceptions;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Atraccion.Microservicios.Atraccion.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/atracciones")]
    public class AtraccionController : ControllerBase
    {
        private readonly IAtraccionBusinessService _service;
        private readonly IResenaBusinessService _resenaBusinessService;

        public AtraccionController(IAtraccionBusinessService service, IResenaBusinessService resenaBusinessService)
        {
            _service = service;
            _resenaBusinessService = resenaBusinessService;
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
            return Ok(ApiResponse<FiltrosDisponibles>.Ok(data, "Filtros disponibles obtenidos exitosamente"));
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

        [HttpGet("{guid:guid}/horarios")]
        public async Task<IActionResult> GetHorariosProximosByAttraction(string guid)
        {
            var data = await _service.GetHorariosByAttraction(guid);
            return Ok(ApiResponse<List<HorarioDto>>.Ok(data, "Listado de cupos con horarios por atracción"));
        }

        [HttpGet("{guid:guid}/horarios/{horarioId:int}/tickets")]
        public async Task<IActionResult> GetTicketsPorHorario(string guid, int horarioId)
        {
            var data = await _service.GetTicketsByHorario(guid, horarioId);
            return Ok(ApiResponse<List<TicketDto>>.Ok(data, "Tickets disponibles para el horario"));
        }

        // Resenia contratos
        [HttpGet("{guid:guid}/resenias")]
        public async Task<IActionResult> GetReseniasByAtraccion(string guid)
        {
            var data = await _resenaBusinessService.GetByAtraccionAsync(guid);
            return Ok(ApiResponse<IEnumerable<ResenaResponse>>.Ok(data, "Listado de reseñas para la atracción"));
        }

        [HttpPost("{guid:guid}/resenias")]
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> CreateResena(string guid, CreateResenaRequest request)
        {
            var clienteId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (clienteId == null)
            {
                throw new UnauthorizedBusinessException("Cliente ID is missing");
            }
            request.ClienteId = int.Parse(clienteId);
            request.Comentario = request.Comentario?.Trim();
            request.AtraccionGuid = guid;

            var data = await _resenaBusinessService.CreateAsync(request);
            return Ok(ApiResponse<int>.Ok(data, "Reseña creada exitosamente"));
        }
    }
}
