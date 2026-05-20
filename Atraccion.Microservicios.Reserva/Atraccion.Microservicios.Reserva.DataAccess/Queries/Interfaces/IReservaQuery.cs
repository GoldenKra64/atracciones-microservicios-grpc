using Atraccion.Microservicios.Reserva.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataAccess.Queries.Interfaces
{
    public interface IReservaQuery
    {
        Task<PagedResult<Reserva.DataAccess.Entities.Reserva>> GetByClienteAsync(int clienteId, int page, int size);
        Task<Reserva.DataAccess.Entities.Reserva?> GetDetalleAsync(int reservaId);
        Task<Reserva.DataAccess.Entities.Reserva?> GetByIdAsync(string id);
        Task<List<Reserva.DataAccess.Entities.Reserva?>> GetAllAsync();
        Task<PagedResult<Reserva.DataAccess.Entities.Reserva>> GetAllBookingAsync(int page, int size);
    }
}
