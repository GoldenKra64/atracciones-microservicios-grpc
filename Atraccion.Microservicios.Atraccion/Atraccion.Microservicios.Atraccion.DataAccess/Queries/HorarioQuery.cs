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
    public class HorarioQuery : IHorarioQuery
    {
        private readonly AtraccionesDbContext _context;

        public HorarioQuery(AtraccionesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Horario>> GetAllAsync()
        {
            return await _context.Horarios
                .Where(x => x.HorEstado == "ACT")
                .OrderBy(x => x.HorId)
                .ToListAsync();
        }

        public async Task<Horario> GetByGuidAsync(string id)
        {
            return await _context.Horarios.Where(x => x.HorGuid == id).FirstOrDefaultAsync();
        }
    }
}
