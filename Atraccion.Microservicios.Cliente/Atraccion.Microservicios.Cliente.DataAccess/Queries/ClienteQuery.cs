using Atraccion.Microservicios.Cliente.DataAccess.Context;
using Atraccion.Microservicios.Cliente.DataAccess.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataAccess.Queries
{
    public class ClienteQuery : IClienteQuery
    {
        private readonly AtraccionesDbContext _context;

        public ClienteQuery(AtraccionesDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente.DataAccess.Entities.Cliente?> GetByUsuarioAsync(int usuarioId)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(x => x.UsuId == usuarioId);
        }
    }
}
