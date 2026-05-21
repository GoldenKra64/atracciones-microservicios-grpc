using Atraccion.Microservicios.Reserva.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataAccess.Repositories.Interfaces
{
    public interface IReservaRepository : IRepository<Reserva.DataAccess.Entities.Reserva>
    {
        Task<int> CreateWithDetallesAsync(Reserva.DataAccess.Entities.Reserva reserva);
        Task<Reserva.DataAccess.Entities.Reserva> UpdateAsync(Reserva.DataAccess.Entities.Reserva reserva);
        Task SoftDeleteAsync(string id);
        Task DeleteDetalleAsync(DetalleReserva detalle);
        Task ApproveAsync(string id, string? atNombre = null);
        Task CancelAsync(string id);
    }
}