using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces
{
    public interface ITagQuery
    {
        Task<List<Tag>> GetAllAsync();
    }
}
