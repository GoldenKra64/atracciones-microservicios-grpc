using Asp.Versioning;
using Atraccion.Microservicios.Cliente.Api.Models.Common;
using Atraccion.Microservicios.Cliente.Business.DTOs.Cliente;
using Atraccion.Microservicios.Cliente.Business.Exceptions;
using Atraccion.Microservicios.Cliente.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Atraccion.Microservicios.Cliente.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteBusinessService _service;

        public ClienteController(IClienteBusinessService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<ClienteResponse>.Ok(data));
        }
        [HttpGet("profile")]
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> GetProfile()
        {
            var id = User.Claims.FirstOrDefault(c => ClaimTypes.NameIdentifier == c.Type)?.Value;

            if (id == null)
            {
                throw new UnauthorizedBusinessException("Cliente ID is missing");
            }

            int cliId = int.Parse(id);

            var data = await _service.GetByIdAsync(cliId);
            return Ok(ApiResponse<ClienteResponse>.Ok(data));
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ClienteResponse>>.Ok(data));
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create(CreateClienteRequest request)
        {
            var id = await _service.CreateAsync(request);
            return Ok(ApiResponse<int>.Ok(id, "Cliente creado"));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(UpdateClienteRequest request, int id)
        {
            request.Id = id;
            await _service.UpdateAsync(request);
            return Ok(ApiResponse<string>.Ok("OK", "Cliente actualizado"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.LogicalDeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("OK", "Cliente eliminado"));
        }
    }
}
