using Atraccion.Microservicios.Auth.DataAccess.Context;
using Atraccion.Microservicios.Auth.DataAccess.Entities;
using Atraccion.Microservicios.Auth.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataAccess.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AtraccionesDbContext context) : base(context) { }

        public async Task<Usuario?> LoginAsync(string login, string password)
        {
            return await _context.Usuarios
                .Include(x => x.UsuarioRoles)
                    .ThenInclude(x => x.Rol)
                    .FirstOrDefaultAsync(x =>
                        x.UsuLogin == login &&
                        x.UsuPasswordHash == password &&
                        x.UsuEstado == "ACT");
        }
    }
}
