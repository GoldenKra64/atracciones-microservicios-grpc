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
    public class ResenaQuery : IResenaQuery
    {
        private readonly AtraccionesDbContext _context;

        public ResenaQuery(AtraccionesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Resena>> GetByAtraccionAsync(int atraccionId)
        {
            return await _context.Resenas
                .Include(r => r.Atraccion)
                .Where(r => r.AtId == atraccionId)
                .OrderByDescending(r => r.ResenaFechaCreacion)
                .ToListAsync();
        }
    }
}
