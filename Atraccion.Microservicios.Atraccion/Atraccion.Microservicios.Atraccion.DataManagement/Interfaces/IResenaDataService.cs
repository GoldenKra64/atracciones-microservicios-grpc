using Atraccion.Microservicios.Atraccion.DataManagement.Models.Resena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Interfaces
{
    public interface IResenaDataService
    {
        Task<List<ResenaModel>> GetByAtraccionAsync(string atraccionId);

        Task<int> CreateAsync(ResenaCreateModel model);

        Task UpdateAsync(ResenaUpdateModel model);

        Task SoftDeleteAsync(int id);
    }
}
