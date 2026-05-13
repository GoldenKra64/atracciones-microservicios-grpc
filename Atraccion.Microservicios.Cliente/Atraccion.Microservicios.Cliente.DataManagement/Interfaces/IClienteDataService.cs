using Atraccion.Microservicios.Cliente.DataManagement.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataManagement.Interfaces
{
    public interface IClienteDataService
    {
        Task<ClienteModel?> GetByUsuarioAsync(int usuarioId);
        Task<ClienteModel?> GetByIdAsync(int id);
        Task<IEnumerable<ClienteModel>> GetAllAsync();

        Task<int> CreateAsync(ClienteCreateModel model);

        Task UpdateAsync(ClienteUpdateModel model);

        Task SoftDeleteAsync(int id);
    }
}
