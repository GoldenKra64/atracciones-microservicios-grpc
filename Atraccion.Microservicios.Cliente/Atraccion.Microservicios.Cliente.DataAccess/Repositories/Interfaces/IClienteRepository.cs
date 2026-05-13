using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataAccess.Repositories.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente.DataAccess.Entities.Cliente>
    {
        Task<Cliente.DataAccess.Entities.Cliente?> GetByIdAsync(int id);
        Task<IEnumerable<Cliente.DataAccess.Entities.Cliente>> GetAllAsync();
    }
}
