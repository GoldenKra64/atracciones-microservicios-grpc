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
    public class TicketQuery : ITicketQuery
    {
        private readonly AtraccionesDbContext _context;

        public TicketQuery(AtraccionesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket?>> GetAllAsync()
        {
            return await _context.Tickets.Where(c => c.TicEstado == "ACT").ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.Horario)
                    .ThenInclude(h => h.Atraccion)
                .Where(c => c.TicId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Ticket?> GetByGuidAsync(string id)
        {
            return await _context.Tickets
                .Include(t => t.Horario)
                    .ThenInclude(h => h.Atraccion)
                .Where(c => c.TicGuid == id)
                .FirstOrDefaultAsync();
        }
    }
}
