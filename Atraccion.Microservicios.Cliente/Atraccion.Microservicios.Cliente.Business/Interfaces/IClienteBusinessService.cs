using Atraccion.Microservicios.Cliente.Business.DTOs.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.Business.Interfaces
{
    public interface IClienteBusinessService
    {
        Task<ClienteResponse> GetByIdAsync(int id);

        Task<IEnumerable<ClienteResponse>> GetAllAsync();

        Task<int> CreateAsync(CreateClienteRequest request);

        Task UpdateAsync(UpdateClienteRequest request);

        Task LogicalDeleteAsync(int id);
    }
}
