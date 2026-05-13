using Atraccion.Microservicios.Atraccion.DataManagement.Models.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Interfaces
{
    public interface ICategoriaDataService
    {
        Task<List<CategoriaModel>> GetTreeAsync();

        Task<int> CreateAsync(CategoriaCreateModel model);

        Task UpdateAsync(CategoriaUpdateModel model);

        Task SoftDeleteAsync(int id);
    }
}
