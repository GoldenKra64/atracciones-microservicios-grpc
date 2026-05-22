using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Mappers
{
    public static class TicketMapper
    {
        public static TicketModel ToModel(Ticket entity)
        {
            return new TicketModel
            {
                Id = entity.TicId,
                Guid = entity.TicGuid,
                Estado = entity.TicEstado,
                Nombre = entity.TicTitulo,
                Precio = entity.TicPrecio,
                Tipo = entity.TicTipoParticipante,
                HorarioId = entity.HorId,
                HorarioGuid = entity.Horario?.HorGuid,

                Horario = entity.Horario != null ? HorarioMapper.ToModel(entity.Horario) : null
            };
        }

        public static Ticket ToEntity(TicketCreateModel model)
        {
            return new Ticket
            {
                HorId = model.HorarioId,
                TicGuid = Guid.NewGuid().ToString(),
                TicTitulo = model.Nombre,
                TicPrecio = model.Precio,
                TicTipoParticipante = model.Tipo,

                TicFechaIngreso = DateTime.UtcNow,
                TicUsuarioIngreso = "system", // Este valor debería ser dinámico en un entorno real
                TicIpIngreso = "127.0.0.1",
                TicEstado = "ACT"
            };
        }
        public static void UpdateEntity(Ticket entity, TicketUpdateModel model)
        {
            entity.HorId = model.HorarioId;
            entity.TicTitulo = model.Nombre;
            entity.TicPrecio = model.Precio;
            entity.TicTipoParticipante = model.Tipo;
        }
    }
}
