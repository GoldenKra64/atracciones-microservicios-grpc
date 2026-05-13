using Atraccion.Microservicios.Factura.DataAccess.Common;
using Atraccion.Microservicios.Factura.DataAccess.Context;
using Atraccion.Microservicios.Factura.DataAccess.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.DataAccess.Queries
{
    public class FacturaQuery : IFacturaQuery
    {
        private readonly AtraccionesDbContext _context;

        public FacturaQuery(AtraccionesDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Factura.DataAccess.Entities.Factura>> GetAllByClienteAsync(int cliId, int page, int size)
        {
            var query = _context.Facturas
                .Where(f => f.CliId == cliId);

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.FacFechaEmision)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PagedResult<Factura.DataAccess.Entities.Factura>
            {
                Items = items,
                TotalRecords = total,
                PageNumber = page,
                PageSize = size
            };
        }
        public async Task<List<Factura.DataAccess.Entities.Factura>> GetAllFacturasAsync()
        {
            return await _context.Facturas
                .OrderBy(x => x.FacFechaEmision)
                .ToListAsync();
        }
    }
}
