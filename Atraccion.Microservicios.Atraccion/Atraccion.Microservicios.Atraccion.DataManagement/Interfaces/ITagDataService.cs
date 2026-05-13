using Atraccion.Microservicios.Atraccion.DataManagement.Models.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Interfaces
{
    public interface ITagDataService
    {
        Task<List<TagModel>> GetAllAsync();
    }
}
