using Atraccion.Microservicios.Atraccion.DataManagement.Models.NoIncluye;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Interfaces
{
    public interface INoIncluyeDataService
    {
        Task<int> CreateAsync(NoIncluyeCreateModel model);

        Task UpdateAsync(NoIncluyeUpdateModel model);

        Task<IEnumerable<NoIncluyeModel>> GetAllAsync();
        Task<NoIncluyeModel> GetByIdAsync(int id);

        Task SoftDeleteAsync(int id);
    }
}
