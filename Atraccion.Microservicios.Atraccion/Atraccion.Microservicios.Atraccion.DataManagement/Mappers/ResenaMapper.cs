using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Resena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Mappers
{
    public static class ResenaMapper
    {
        public static ResenaModel ToModel(Resena entity)
        {
            return new ResenaModel
            {
                ClienteId = entity.CliId,
                AtraccionId = entity.AtId,
                Calificacion = entity.ResenaCalificacion,
                Comentario = entity.ResenaComentario,
                Fecha = entity.ResenaFechaCreacion.ToShortDateString()
            };
        }

        public static Resena ToEntity(ResenaCreateModel model)
        {
            return new Resena
            {
                CliId = model.ClienteId.Value,
                ResenaGuid = Guid.NewGuid().ToString(),
                AtId = model.AtraccionId,
                ResenaCalificacion = model.Calificacion,
                ResenaComentario = model.Comentario,
                ResenaFechaCreacion = DateTime.UtcNow,
                ResenaIpCreacion = "127.0.0.1",
                ResenaUsuarioCreacion = "system",
                ResenaEstado = "ACT"
            };
        }
    }
}
