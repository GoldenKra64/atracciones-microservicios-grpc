using Asp.Versioning;
using Atraccion.Microservicios.Atraccion.Api.Models.Common;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Resena;
using Atraccion.Microservicios.Atraccion.Business.Exceptions;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Atraccion.Microservicios.Atraccion.Api.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v2/[controller]")]
    public class ResenaController : ControllerBase
    {
        private readonly IResenaBusinessService _service;

        public ResenaController(IResenaBusinessService service)
        {
            _service = service;
        }

        [HttpGet("atraccion/{id}")]
        public async Task<IActionResult> GetByAtraccion(string id)
        {
            var data = await _service.GetByAtraccionAsync(id);
            return Ok(ApiResponse<IEnumerable<ResenaResponse>>.Ok(data));
        }

        [HttpPost]
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> Create(string guid, CreateResenaRequest request)
        {
            var clienteId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (clienteId == null)
            {
                throw new UnauthorizedBusinessException("Cliente ID is missing");
            }

            request.ClienteId = int.Parse(clienteId);
            request.AtraccionGuid = guid;

            var id = await _service.CreateAsync(request);
            return Ok(ApiResponse<int>.Ok(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateResenaRequest request)
        {
            var clienteId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (clienteId == null)
            {
                throw new UnauthorizedBusinessException("Cliente ID is missing");
            }

            if (clienteId != request.ClienteId.ToString())
            {
                throw new UnauthorizedBusinessException("No puedes modificar una reseña que no sea tuya");
            }


            request.ClienteId = int.Parse(clienteId);

            await _service.UpdateAsync(request);
            return Ok(ApiResponse<string>.Ok("OK"));
        }
    }
}
