using Atraccion.Microservicios.Atraccion.Business.DTOs.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Interfaces
{
    public interface ICategoriaBusinessService
    {
        Task<IEnumerable<CategoriaResponse>> GetAllAsync();
    }
}
