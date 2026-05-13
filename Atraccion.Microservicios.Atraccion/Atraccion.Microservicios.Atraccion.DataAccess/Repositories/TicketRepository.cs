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
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(AtraccionesDbContext context) : base(context)
        {
        }

        public Task<Ticket?> GetByIdAsync(string tickGuid)
        {
            return _context.Tickets.FirstOrDefaultAsync(t => t.TicGuid == tickGuid);
        }
    }
}
