using Atraccion.Microservicios.Reserva.DataManagement.Interfaces;
using Atraccion.Microservicios.Reserva.DataManagement.Protos;
using System;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Integrations
{
    public class FacturaIntegrationGrpc : IFacturaIntegration
    {
        private readonly FacturaService.FacturaServiceClient _client;

        public FacturaIntegrationGrpc(FacturaService.FacturaServiceClient client)
        {
            _client = client;
        }

        public async Task GenerateInvoiceAsync(GenerateInvoiceDto dto)
        {
            await _client.GenerateInvoiceAsync(new GenerateInvoiceRequest
            {
                RevId = dto.RevId,
                CliId = dto.CliId,
                Total = dto.Total,
                Canal = dto.Canal,
                NombreReceptor = dto.NombreReceptor ?? string.Empty,
                CorreoReceptor = dto.CorreoReceptor ?? string.Empty
            });
        }
    }
}
