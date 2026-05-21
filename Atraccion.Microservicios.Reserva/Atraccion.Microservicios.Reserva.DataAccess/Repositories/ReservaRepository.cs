using Atraccion.Microservicios.Reserva.DataAccess.Context;
using Atraccion.Microservicios.Reserva.DataAccess.Entities;
using Atraccion.Microservicios.Reserva.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataAccess.Repositories
{
    public class ReservaRepository : Repository<Reserva.DataAccess.Entities.Reserva>, IReservaRepository
    {
        public ReservaRepository(AtraccionesDbContext context) : base(context) { }

        public async Task<int> CreateWithDetallesAsync(Reserva.DataAccess.Entities.Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
            return reserva.RevId;
        }

        public async Task<Reserva.DataAccess.Entities.Reserva> UpdateAsync(Reserva.DataAccess.Entities.Reserva reserva)
        {
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();
            return reserva;
        }

        public Task DeleteDetalleAsync(DetalleReserva detalle)
        {
            _context.Set<DetalleReserva>().Remove(detalle);
            return Task.CompletedTask;
        }

        public async Task SoftDeleteAsync(string id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Detalles)
                .FirstOrDefaultAsync(r => r.RevGuid == id);

            if (reserva == null) return;

            reserva.RevEstado = "ANU";

            await _context.SaveChangesAsync();
        }

        public async Task ApproveAsync(string id, string? atNombre = null)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Detalles)
                .FirstOrDefaultAsync(r => r.RevGuid == id);

            if (reserva == null) return;

            reserva.RevEstado = "APR";

            if (!string.IsNullOrEmpty(atNombre) && string.IsNullOrEmpty(reserva.AtNombre))
                reserva.AtNombre = atNombre;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reserva.DataAccess.Entities.Reserva>> GetByClienteAsync(int clienteId, int page, int size)
        {
            return await _context.Reservas
                .Where(r => r.CliId == clienteId && r.RevEstado == "ACT")
                .Include(r => r.Detalles)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task CancelAsync(string id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Detalles)
                .FirstOrDefaultAsync(r => r.RevGuid == id);

            if (reserva == null) return;

            reserva.RevEstado = "ANU";

            await _context.SaveChangesAsync();
        }
    }
}
