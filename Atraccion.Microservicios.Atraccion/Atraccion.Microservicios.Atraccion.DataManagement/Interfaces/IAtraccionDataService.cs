using Atraccion.Microservicios.Atraccion.DataManagement.Models;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Atraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Interfaces
{
    public interface IAtraccionDataService
    {
        Task<DataPagedResult<AtraccionModel>> GetPagedAsync(
            FiltroModel filtro);

        Task<AtraccionModel?> GetByIdAsync(string id);
        Task<AtraccionModel?> GetInternalByIdAsync(string id);
        Task<List<AtraccionModel?>> GetAllInternalAsync();

        Task CreateAsync(AtraccionCreateModel model);

        Task UpdateAsync(AtraccionUpdateModel model);

        Task SoftDeleteAsync(string id);

        Task<List<AtraccionTypeModel?>> GetAtraccionTypeAsync();
    }
}
