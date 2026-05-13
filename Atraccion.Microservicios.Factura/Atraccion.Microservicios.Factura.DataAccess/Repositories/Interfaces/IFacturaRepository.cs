using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.DataAccess.Repositories.Interfaces
{
    public interface IFacturaRepository : IRepository<Factura.DataAccess.Entities.Factura>
    {
        Task<Factura.DataAccess.Entities.Factura?> GetByReservaAsync(int reservaId);
    }
}
