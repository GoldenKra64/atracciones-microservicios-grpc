using Atraccion.Microservicios.Factura.DataManagement.Models;
using Atraccion.Microservicios.Factura.DataManagement.Models.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.DataManagement.Interfaces
{
    public interface IFacturaDataService
    {
        Task<FacturaModel?> GetByReservaAsync(int reservaId);
        Task<DataPagedResult<FacturaModel?>> GetByClienteAsync(int cliId, int page, int size);
        Task<List<FacturaModel>> GetAllAsync();
    }
}
