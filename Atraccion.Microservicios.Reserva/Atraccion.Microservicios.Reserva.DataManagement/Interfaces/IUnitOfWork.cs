using Atraccion.Microservicios.Reserva.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IReservaRepository ReservaRepository { get; }

        Task<int> SaveChangesAsync();

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
