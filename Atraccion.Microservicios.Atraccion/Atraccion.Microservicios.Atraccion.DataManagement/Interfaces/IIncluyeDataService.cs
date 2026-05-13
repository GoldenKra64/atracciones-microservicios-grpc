using Atraccion.Microservicios.Atraccion.DataManagement.Models.Incluye;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Interfaces
{
    public interface IIncluyeDataService
    {
        Task<int> CreateAsync(IncluyeCreateModel model);

        Task UpdateAsync(IncluyeUpdateModel model);

        Task<IEnumerable<IncluyeModel>> GetAllAsync();
        Task<IncluyeModel> GetByIdAsync(int id);

        Task SoftDeleteAsync(int id);
    }
}
