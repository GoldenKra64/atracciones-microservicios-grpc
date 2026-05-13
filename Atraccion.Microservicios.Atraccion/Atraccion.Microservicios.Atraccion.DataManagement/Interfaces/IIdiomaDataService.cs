using Atraccion.Microservicios.Atraccion.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Interfaces
{
    public interface IIdiomaDataService
    {
        Task<List<IdiomaModel>> GetAllAsync();
    }
}
