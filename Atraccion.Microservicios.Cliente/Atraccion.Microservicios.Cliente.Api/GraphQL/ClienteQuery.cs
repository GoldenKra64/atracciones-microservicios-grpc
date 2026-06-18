using System.Security.Claims;
using System.Net.Http.Headers;
using Atraccion.Microservicios.Cliente.Business.DTOs.Cliente;
using Atraccion.Microservicios.Cliente.Business.Exceptions;
using Atraccion.Microservicios.Cliente.Business.Interfaces;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Atraccion.Microservicios.Cliente.Api.GraphQL
{
    public class FacturaResponseDto
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
    }

    public class PagedFacturaResponse
    {
        public List<FacturaResponseDto> Items { get; set; }
    }

    public class ApiResponsePagedFactura
    {
        public PagedFacturaResponse Data { get; set; }
    }

    public class ClienteQuery
    {
        [Authorize(Roles = new[] { "CLIENTE" })]
        public async Task<ClienteResponse> GetProfile(
            [Service] IClienteBusinessService service,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            var id = user?.Claims.FirstOrDefault(c => ClaimTypes.NameIdentifier == c.Type)?.Value;

            if (id == null)
            {
                throw new UnauthorizedBusinessException("Cliente ID is missing");
            }

            int cliId = int.Parse(id);
            return await service.GetByIdAsync(cliId);
        }

        [Authorize(Roles = new[] { "CLIENTE" })]
        public async Task<List<FacturaResponseDto>> GetFacturas(
            int page, 
            int limit,
            [Service] IHttpClientFactory httpClientFactory,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            var client = httpClientFactory.CreateClient();
            var httpContext = httpContextAccessor.HttpContext;
            var token = httpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Llamar al microservicio de Factura
            var url = $"{configuration["Facturas:BaseUrl"]}/api/v2/facturas/mis-facturas?page={page}&limit={limit}";
            
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponsePagedFactura>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result?.Data?.Items ?? new List<FacturaResponseDto>();
            }

            return new List<FacturaResponseDto>();
        }
    }
}
