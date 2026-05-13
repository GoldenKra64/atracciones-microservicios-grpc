using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Repositories.Interfaces
{
    public interface IAtraccionRepository : IRepository<Atraccion.DataAccess.Entities.Atraccion>
    {
        public Task SoftDeleteAsync(string id);
    }
}
