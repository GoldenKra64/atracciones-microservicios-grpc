using Atraccion.Microservicios.Factura.DataAccess.Repositories.Interfaces;
using Atraccion.Microservicios.Factura.DataManagement.Protos;
using Atracciones.Microservicios.Messages;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.Api.Consumers
{
    public class GenerateInvoiceConsumer : IConsumer<GenerateInvoiceCommand>
    {
        private readonly IFacturaRepository _repo;
        private readonly ClienteService.ClienteServiceClient _clienteClient;

        public GenerateInvoiceConsumer(
            IFacturaRepository repo,
            ClienteService.ClienteServiceClient clienteClient)
        {
            _repo = repo;
            _clienteClient = clienteClient;
        }


        public async Task Consume(ConsumeContext<GenerateInvoiceCommand> context)
        {
            var request = context.Message;
            string? nombreReceptor = request.NombreReceptor;
            string? correoReceptor = request.CorreoReceptor;

            if (string.IsNullOrEmpty(nombreReceptor) && request.CliId > 0)
            {
                try 
                {
                    var clienteReq = new ClienteRequest { CliId = request.CliId };
                    var clienteRes = await _clienteClient.GetClienteByIdAsync(clienteReq);
                    if (clienteRes != null)
                    {
                        nombreReceptor = $"{clienteRes.Nombres} {clienteRes.Apellidos}".Trim();
                        correoReceptor = clienteRes.Correo;
                    }
                } 
                catch (Exception)
                {
                    // Log fallback error if needed
                }
            }

            var factura = new DataAccess.Entities.Factura
            {
                FacGuid = Guid.NewGuid().ToString(),
                RevId = request.RevId,
                CliId = request.CliId,
                FacNombreReceptor = nombreReceptor,
                FacCorreoReceptor = correoReceptor,
                FacNumero = $"FAC-{DateTime.Now:yyyyMMddHHmmss}-{new Random().Next(100, 999)}",
                FacFechaEmision = DateTime.UtcNow,
                FacTotal = (decimal)request.Total,
                FacOrigenCanal = request.Canal,
                FacEstado = "ACT",
                FacUsuarioIngreso = "mq-system",
                FacIpIngreso = "127.0.0.1"
            };

            await _repo.CreateAsync(factura);

            // Respondemos al RequestClient con el resultado
            await context.RespondAsync<GenerateInvoiceResult>(new
            {
                FacGuid = factura.FacGuid,
                FacNumero = factura.FacNumero,
                RevCodigo = "",
                Total = (double)factura.FacTotal,
                FechaEmision = factura.FacFechaEmision,
                NombreReceptor = factura.FacNombreReceptor ?? "",
                CorreoReceptor = factura.FacCorreoReceptor ?? ""
            });
        }
    }
}
