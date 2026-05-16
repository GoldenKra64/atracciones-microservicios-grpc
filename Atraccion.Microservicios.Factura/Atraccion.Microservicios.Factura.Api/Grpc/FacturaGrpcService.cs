using Atraccion.Microservicios.Factura.Api.Protos;
using Atraccion.Microservicios.Factura.DataAccess.Repositories.Interfaces;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.Api.Grpc
{
    public class FacturaGrpcService : FacturaService.FacturaServiceBase
    {
        private readonly IFacturaRepository _repo;
        private readonly Atraccion.Microservicios.Factura.DataManagement.Protos.ClienteService.ClienteServiceClient _clienteClient;

        public FacturaGrpcService(IFacturaRepository repo, Atraccion.Microservicios.Factura.DataManagement.Protos.ClienteService.ClienteServiceClient clienteClient)
        {
            _repo = repo;
            _clienteClient = clienteClient;
        }

        public override async Task<GenerateInvoiceResponse> GenerateInvoice(GenerateInvoiceRequest request, ServerCallContext context)
        {
            try
            {
                string nombreReceptor = request.NombreReceptor;
                string correoReceptor = request.CorreoReceptor;

                if (string.IsNullOrEmpty(nombreReceptor) && request.CliId > 0)
                {
                    try 
                    {
                        var clienteReq = new Atraccion.Microservicios.Factura.DataManagement.Protos.ClienteRequest { CliId = request.CliId };
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
                    FacUsuarioIngreso = "grpc-system",
                    FacIpIngreso = "127.0.0.1"
                };

                await _repo.CreateAsync(factura);

                return new GenerateInvoiceResponse
                {
                    Success = true,
                    FacId = factura.FacId
                };
            }
            catch (Exception ex)
            {
                // Manejo básico de errores, en producción se recomienda un logger
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
}
