using Atraccion.Microservicios.Atraccion.DataManagement.Models.Imagen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Interfaces
{
    public interface IImagenDataService
    {
        Task<ImagenModel> GetByIdAsync(int id);
        Task<List<ImagenModel>> GetAllAsync();
        Task UpdateAsync(ImagenUpdateModel model);
        Task<int> CreateAsync(ImagenCreateModel model);
        Task SoftDeleteAsync(int id);
    }
}
