using Atraccion.Microservicios.Factura.DataAccess.Context;
using Atraccion.Microservicios.Factura.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.DataAccess.Repositories
{
    public class FacturaRepository : Repository<Factura.DataAccess.Entities.Factura>, IFacturaRepository
    {
        public FacturaRepository(AtraccionesDbContext context) : base(context) { }

        public async Task<Factura.DataAccess.Entities.Factura?> GetByReservaAsync(int reservaId)
        {
            return await _context.Facturas.FirstOrDefaultAsync(f => f.RevId == reservaId);
        }
    }
}
