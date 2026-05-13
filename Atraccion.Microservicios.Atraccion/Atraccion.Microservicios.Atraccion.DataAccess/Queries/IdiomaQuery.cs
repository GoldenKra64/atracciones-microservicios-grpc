using Atraccion.Microservicios.Atraccion.DataAccess.Context;
using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Queries
{
    public class IdiomaQuery : IIdiomaQuery
    {
        private readonly AtraccionesDbContext _context;

        public IdiomaQuery(AtraccionesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Idioma>> GetAllAsync()
        {
            return await _context.Idiomas
                .OrderBy(x => x.IdNombre)
                .ToListAsync();
        }
    }
}
