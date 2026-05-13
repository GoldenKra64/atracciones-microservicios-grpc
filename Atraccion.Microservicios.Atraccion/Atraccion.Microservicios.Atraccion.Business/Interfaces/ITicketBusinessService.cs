using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Interfaces
{
    public interface ITicketBusinessService
    {
        Task<List<TicketRes>> GetAllAsync();
        Task<TicketRes> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateTicketRequest request);

        Task UpdateAsync(UpdateTicketRequest request);

        Task LogicalDeleteAsync(int id);
    }
}
