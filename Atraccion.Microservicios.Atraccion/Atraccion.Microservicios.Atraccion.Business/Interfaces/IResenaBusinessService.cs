using Atraccion.Microservicios.Atraccion.Business.DTOs.Resena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Interfaces
{
    public interface IResenaBusinessService
    {
        Task<IEnumerable<ResenaResponse>> GetByAtraccionAsync(string atraccionId);

        Task<int> CreateAsync(CreateResenaRequest request);

        Task UpdateAsync(UpdateResenaRequest request);

        Task LogicalDeleteAsync(int id);
    }
}
