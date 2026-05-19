using Atraccion.Microservicios.Reserva.DataManagement.Integrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Interfaces
{
    public interface IAtraccionIntegration
    {
        Task<TicketIntegrationDto> GetTicketInfoAsync(int ticId);
        Task<TicketIntegrationDto> GetTicketInfoByGuidAsync(string ticGuid);
        Task<HorarioIntegrationDto> GetHorarioByGuidAsync(string guid);
        Task ConsumeCapacityAsync(int horId, int cantidad);
    }
}
