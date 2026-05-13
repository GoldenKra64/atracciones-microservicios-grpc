using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces
{
    public interface IDestinoQuery
    {
        Task<List<Destino>> GetAllAsync();
        Task<Destino> GetByIdAsync(int id);
    }
}
