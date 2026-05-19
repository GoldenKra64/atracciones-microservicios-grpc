using Asp.Versioning;
using Atraccion.Microservicios.Atraccion.Api.Models.Common;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Tag;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Atraccion.Microservicios.Atraccion.Api.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v2/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagBusinessService _service;

        public TagController(ITagBusinessService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<TagResponse>>.Ok(data));
        }
    }
}
