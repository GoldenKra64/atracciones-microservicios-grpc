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

        public FacturaGrpcService(IFacturaRepository repo)
        {
            _repo = repo;
        }

        public override async Task<GenerateInvoiceResponse> GenerateInvoice(GenerateInvoiceRequest request, ServerCallContext context)
        {
            try
            {
                var factura = new DataAccess.Entities.Factura
                {
                    FacGuid = Guid.NewGuid().ToString(),
                    RevId = request.RevId,
                    CliId = request.CliId,
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
