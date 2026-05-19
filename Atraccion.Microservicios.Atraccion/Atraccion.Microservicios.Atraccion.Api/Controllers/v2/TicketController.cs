using Asp.Versioning;
using Atraccion.Microservicios.Atraccion.Api.Models.Common;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atraccion.Microservicios.Atraccion.Api.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v2/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketBusinessService _service;
        private readonly IHorarioBusinessService _horarioService;

        public TicketController(ITicketBusinessService service, IHorarioBusinessService _horarioService)
        {
            _service = service;
            this._horarioService = _horarioService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<List<TicketRes>>.Ok(data, "Tickets traidos con éxito"));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<TicketRes>.Ok(data, "Tickets traidos con éxito"));
        }


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create(CreateTicketRequest request)
        {
            var id = await _service.CreateAsync(request);
            return Ok(ApiResponse<int>.Ok(id));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(UpdateTicketRequest request, int id)
        {
            request.Id = id;
            await _service.UpdateAsync(request);
            return Ok(ApiResponse<string>.Ok("OK"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.LogicalDeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("OK"));
        }

        [HttpGet("{guid:guid}/horarios")]
        public async Task<IActionResult> GetHorariosByTickets(string guid)
        {
            var data = await _service.GetHorariosByTicketAsync(guid);
            return Ok(ApiResponse<HorarioDto>.Ok(data, "Lista de horarios para el ticket", 200));
        }
    }
}
