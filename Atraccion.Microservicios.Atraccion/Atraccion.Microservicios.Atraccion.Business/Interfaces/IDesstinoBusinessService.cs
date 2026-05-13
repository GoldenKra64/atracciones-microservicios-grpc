using Atraccion.Microservicios.Atraccion.Business.DTOs.Destino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Interfaces
{
    public interface IDestinoBusinessService
    {
        Task<IEnumerable<DestinoResponse>> GetAllAsync();
        Task<DestinoResponse> GetByIdAsync(int id);

        Task<int> CreateAsync(CreateDestinoRequest request);

        Task UpdateAsync(UpdateDestinoRequest request);

        Task LogicalDeleteAsync(int id);
    }
}
