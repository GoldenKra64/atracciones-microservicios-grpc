using Atraccion.Microservicios.Atraccion.DataAccess.Context;
using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Repositories
{
    public class IncluyeRepository : Repository<Incluye>, IIncluyeRepository
    {
        public IncluyeRepository(AtraccionesDbContext context) : base(context) { }

        public async Task<List<Incluye>> GetAllAsync()
        {
            return await _context.Incluyes
                .Where(i => i.IncEstado == "ACT")
                .ToListAsync();
        }
    }
}
