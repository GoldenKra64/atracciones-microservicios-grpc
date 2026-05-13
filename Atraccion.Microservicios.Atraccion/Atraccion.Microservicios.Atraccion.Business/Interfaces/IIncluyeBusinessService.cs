using Atraccion.Microservicios.Atraccion.Business.DTOs.Incluye;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Interfaces
{
    public interface IIncluyeBusinessService
    {
        Task<IEnumerable<IncluyeResponse>> GetAllAsync();
        Task<IncluyeResponse> GetByIdAsync(int id);

        Task<int> CreateAsync(CreateIncluyeRequest request);

        Task UpdateAsync(UpdateIncluyeRequest request);

        Task LogicalDeleteAsync(int id);
    }
}
