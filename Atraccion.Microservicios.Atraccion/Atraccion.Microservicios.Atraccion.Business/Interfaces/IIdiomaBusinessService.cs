using Atraccion.Microservicios.Atraccion.Business.DTOs.Idioma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Interfaces
{
    public interface IIdiomaBusinessService
    {
        Task<IEnumerable<IdiomaResponse>> GetAllAsync();
    }
}
