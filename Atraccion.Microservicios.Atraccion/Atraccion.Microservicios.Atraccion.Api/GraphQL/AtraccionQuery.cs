using Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;

namespace Atraccion.Microservicios.Atraccion.Api.GraphQL
{
    public class AtraccionQuery
    {
        public async Task<AtraccionDetalleDto> GetAtraccion([Service] IAtraccionBusinessService service, string guid)
        {
            return await service.GetByIdAsync(guid);
        }

        public async Task<List<HorarioDto>> GetHorarios([Service] IAtraccionBusinessService service, string guid)
        {
            return (await service.GetHorariosByAttraction(guid)).ToList();
        }

        public async Task<List<TicketDto>> GetTickets([Service] IAtraccionBusinessService service, string guid, string horarioId)
        {
            var data = await service.GetTicketsByHorario(guid, horarioId);
            return data.ToList();
        }
    }
}
