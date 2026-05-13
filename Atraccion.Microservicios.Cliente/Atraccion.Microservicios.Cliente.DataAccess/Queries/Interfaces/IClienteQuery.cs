using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataAccess.Queries.Interfaces
{
    public interface IClienteQuery
    {
        Task<Entities.Cliente?> GetByUsuarioAsync(int usuarioId);
    }
}
