using Atraccion.Microservicios.Reserva.DataAccess.Common;
using Atraccion.Microservicios.Reserva.DataAccess.Context;
using Atraccion.Microservicios.Reserva.DataAccess.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataAccess.Queries
{
    public class ReservaQuery : IReservaQuery
    {
        private readonly AtraccionesDbContext _context;

        public ReservaQuery(AtraccionesDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Reserva.DataAccess.Entities.Reserva>> GetByClienteAsync(int clienteId, int page, int size)
        {
            var query = _context.Reservas
                .Include(r => r.Detalles)
                /*
                    .ThenInclude(d => d.Ticket)
                .Include(r => r.Factura)
                */
                .Where(r => r.CliId == clienteId);

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.RevFechaReservaUtc)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PagedResult<Reserva.DataAccess.Entities.Reserva>
            {
                Items = items,
                TotalRecords = total,
                PageNumber = page,
                PageSize = size
            };
        }

        public async Task<Reserva.DataAccess.Entities.Reserva?> GetDetalleAsync(int reservaId)
        {
            return await _context.Reservas
                .Include(r => r.Detalles)
                /*
                    .ThenInclude(d => d.Ticket)
                        .ThenInclude(t => t.Horario)
                            .ThenInclude(h => h.Atraccion)
                .Include(r => r.Factura)
                */
                .FirstOrDefaultAsync(r => r.RevId == reservaId);
        }

        public async Task<Reserva.DataAccess.Entities.Reserva?> GetByIdAsync(string id)
        {
            return await _context.Reservas
                .Include(r => r.Detalles)
                /*
                    .ThenInclude(d => d.Ticket)
                        .ThenInclude(t => t.Horario)
                            .ThenInclude(h => h.Atraccion).AsTracking()
                */
                .FirstOrDefaultAsync(r => r.RevGuid == id);
        }

        public async Task<List<Reserva.DataAccess.Entities.Reserva?>> GetAllAsync()
        {
            return await _context.Reservas
                .Include(r => r.Detalles)
                            /*
                                .ThenInclude(x => x.Ticket)
                                    .ThenInclude(x => x.Horario)
                                        .ThenInclude(x => x.Atraccion)*/.Where(x => x.RevEstado == "PEN").ToListAsync();
        }
    }
}
