using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        public Task<Ticket?> GetByIdAsync(string tickGuid);
    }
}
