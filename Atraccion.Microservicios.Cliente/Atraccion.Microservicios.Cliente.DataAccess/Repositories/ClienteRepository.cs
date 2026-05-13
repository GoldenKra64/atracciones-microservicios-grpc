using Atraccion.Microservicios.Cliente.DataAccess.Context;
using Atraccion.Microservicios.Cliente.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataAccess.Repositories
{
    public class ClienteRepository : Repository<Cliente.DataAccess.Entities.Cliente>, IClienteRepository
    {
        public ClienteRepository(AtraccionesDbContext context) : base(context) { }

        public async Task<IEnumerable<Cliente.DataAccess.Entities.Cliente>> GetAllAsync()
        {
            return await _context.Clientes
                .Where(c => c.CliEstado == "ACT")
                .ToListAsync();
        }
    }
}
