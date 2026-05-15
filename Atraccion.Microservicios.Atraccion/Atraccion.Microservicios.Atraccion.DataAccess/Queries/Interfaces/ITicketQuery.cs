using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces
{
    public interface ITicketQuery
    {
        Task<List<Ticket?>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(int id);
        Task<Ticket?> GetByGuidAsync(string guid);
    }
}
