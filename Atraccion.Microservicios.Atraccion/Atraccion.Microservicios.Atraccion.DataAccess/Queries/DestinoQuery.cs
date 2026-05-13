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
    public class DestinoQuery : IDestinoQuery
    {
        private readonly AtraccionesDbContext _context;

        public DestinoQuery(AtraccionesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Destino>> GetAllAsync()
        {
            return await _context.Destinos
                .OrderBy(x => x.DesNombre)
                .Where(x => x.DesEstado == "ACT")
                .ToListAsync();
        }
        public async Task<Destino> GetByIdAsync(int id)
        {
            return await _context.Destinos.Where(x => x.DesId == id)
                .OrderBy(x => x.DesNombre).FirstOrDefaultAsync();
        }
    }
}
