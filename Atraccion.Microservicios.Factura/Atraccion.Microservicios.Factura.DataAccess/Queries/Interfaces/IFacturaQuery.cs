using Atraccion.Microservicios.Factura.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.DataAccess.Queries.Interfaces
{
    public interface IFacturaQuery
    {
        Task<PagedResult<Factura.DataAccess.Entities.Factura>> GetAllByClienteAsync(int cliId, int page, int size);
        Task<List<Factura.DataAccess.Entities.Factura>> GetAllFacturasAsync();
    }
}
