using Atraccion.Microservicios.Auth.DataAccess.Context;
using Atraccion.Microservicios.Auth.DataAccess.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataAccess.Queries
{
    public class UsuarioQuery : IUsuarioQuery
    {
        private readonly AtraccionesDbContext _context;

        public UsuarioQuery(AtraccionesDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UserIsAlreadyRegistered(string login)
        {
            var check = await _context.Usuarios.Where(c => c.UsuLogin == login).FirstOrDefaultAsync();

            return check != null;
        }
    }
}
