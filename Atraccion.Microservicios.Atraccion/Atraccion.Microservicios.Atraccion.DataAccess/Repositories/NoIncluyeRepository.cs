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
    public class NoIncluyeRepository : Repository<NoIncluye>, INoIncluyeRepository
    {
        public NoIncluyeRepository(AtraccionesDbContext context) : base(context) { }

        public async Task<List<NoIncluye>> GetAllAsync()
        {
            return await _context.NoIncluyes
                .Where(i => i.NoIncEstado == "ACT")
                .ToListAsync();
        }
    }
}
