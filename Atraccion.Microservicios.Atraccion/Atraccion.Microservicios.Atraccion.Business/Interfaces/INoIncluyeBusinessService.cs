using Atraccion.Microservicios.Atraccion.Business.DTOs.NoIncluye;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Interfaces
{
    public interface INoIncluyeBusinessService
    {
        Task<IEnumerable<NoIncluyeResponse>> GetAllAsync();
        Task<NoIncluyeResponse> GetByIdAsync(int id);

        Task<int> CreateAsync(CreateNoIncluyeRequest request);

        Task UpdateAsync(UpdateNoIncluyeRequest request);

        Task LogicalDeleteAsync(int id);
    }
}
