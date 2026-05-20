using Atraccion.Microservicios.Reserva.Business.DTOs;
using Atraccion.Microservicios.Reserva.Business.DTOs.Reserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Atraccion.Microservicios.Reserva.Business.DTOs.Factura;

namespace Atraccion.Microservicios.Reserva.Business.Interfaces
{
    public interface IReservaBusinessService
    {
        Task<ReservaResponse> GetByIdAsync(string id);
        Task<List<ReservaResponse>> GetAllAsync();
        Task<PagedResponse<ReservaResponse>> GetByClienteAsync(
            int clienteId,
            int page,
            int size);

        Task<ReservaResponse> CreateAsync(CreateReservaRequest request);
        Task<ReservaResponse> CreatePublicAsync(CreateReservaRequest request);
        Task<ReservaResponse> UpdateAsync(UpdateReservaRequest request);

        Task LogicalDeleteAsync(string id);
        Task CancelarAsync(string id, CancelarReservaRequest request);
        Task<FacturaResponse> ApproveAsync(string id, ConfirmarPagoRequest request);

        Task<PagedResponse<ReservaResponse>> GetAllBookingAsync(int page, int size);
    }
}
