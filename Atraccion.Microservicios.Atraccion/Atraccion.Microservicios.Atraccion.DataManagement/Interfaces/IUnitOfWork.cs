using Atraccion.Microservicios.Atraccion.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IResenaRepository ResenaRepository { get; }
        ITicketRepository TicketRepository { get; }
        IAtraccionRepository AtraccionRepository { get; }
        IImagenRepository ImagenRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
        IDestinoRepository DestinoRepository { get; }
        IIncluyeRepository IncluyeRepository { get; }
        IHorarioRepository HorarioRepository { get; }

        Task<int> SaveChangesAsync();

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
