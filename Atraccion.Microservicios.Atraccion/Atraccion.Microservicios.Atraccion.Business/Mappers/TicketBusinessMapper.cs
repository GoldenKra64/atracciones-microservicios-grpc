using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Mappers
{
    public static class TicketBusinessMapper
    {
        public static TicketCreateModel ToCreateModel(CreateTicketRequest request)
        {
            return new TicketCreateModel
            {
                Nombre = request.Nombre,
                Precio = request.Precio,
                Tipo = request.Tipo
            };
        }

        public static TicketUpdateModel ToUpdateModel(UpdateTicketRequest request)
        {
            return new TicketUpdateModel
            {
                Id = request.Id,
                Nombre = request.Nombre,
                Precio = request.Precio,
                Tipo = request.Tipo
            };
        }

        public static TicketResponse ToResponse(TicketModel model)
        {
            return new TicketResponse
            {
                Id = model.Id,
                Guid = model.Guid,
                Nombre = model.Nombre,
                Precio = model.Precio,
                Tipo = model.Tipo,
                Horario = HorarioBusinessMapper.ToResponse(model.Horario)
            };
        }
        public static TicketRes ToResponseNoHorario(TicketModel model)
        {
            return new TicketRes
            {
                Id = model.Id,
                Guid = model.Guid,
                Nombre = model.Nombre,
                Precio = model.Precio,
                Tipo = model.Tipo,
                HorarioId = model.HorarioId,
                HorarioGuid = model.HorarioGuid
            };
        }
    }
}
