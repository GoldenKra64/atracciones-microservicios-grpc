using Atraccion.Microservicios.Atraccion.DataManagement.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Interfaces
{
    public interface ITicketDataService
    {
        Task<List<TicketModel>> GetAllAsync();
        Task<TicketModel> GetByIdAsync(int id);
        Task<TicketModel> GetByGuidAsync(string id);
        Task<int> CreateAsync(TicketCreateModel model);

        Task UpdateAsync(TicketUpdateModel model);

        Task SoftDeleteAsync(int id);
    }
}
