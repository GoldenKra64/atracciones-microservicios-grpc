using Atraccion.Microservicios.Factura.Business.DTOs;
using Atraccion.Microservicios.Factura.Business.DTOs.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.Business.Interfaces
{
    public interface IFacturaBusinessService
    {
        Task<FacturaResponse?> GetByReservaAsync(int reservaId);
        Task<PagedResponse<FacturaResponse?>> GetByClienteAsync(int cliId, int page, int size);
        Task<List<FacturaResponse>> GetAllFacturasAsync();
    }
}
