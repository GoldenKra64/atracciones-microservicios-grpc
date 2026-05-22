using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Atraccion;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Horario;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Resena;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Mappers
{
    public static class HorarioMapper
    {
        public static HorarioModel ToModel(Horario entity)
        {
            return new HorarioModel
            {
                HorarioId = entity.HorId,
                HorarioGuid = entity.HorGuid,
                AtraccionId = entity.AtId,
                AtraccionGuid = entity.Atraccion?.AtGuid,
                Fecha = entity.HorFecha.ToString("yyyy-MM-dd"),
                HoraInicio = entity.HorHoraInicio.ToString(@"hh\:mm"),
                HoraFin = entity.HorHoraFin?.ToString(@"hh\:mm"),
                Cupos = entity.HorCuposDisponibles,
                NombreAtraccion = entity.Atraccion?.AtNombre
            };
        }
        public static Horario ToEntity(HorarioModel model)
        {
            return new Horario
            {
                HorGuid = Guid.NewGuid().ToString(),
                HorEstado = "ACT",
                HorFecha = DateTime.ParseExact(model.Fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture),

                HorHoraInicio = TimeSpan.Parse(model.HoraInicio, CultureInfo.InvariantCulture),

                HorHoraFin = model.HoraFin != null
                    ? TimeSpan.Parse(model.HoraFin, CultureInfo.InvariantCulture)
                    : (TimeSpan?)null,

                HorCuposDisponibles = model.Cupos,

                AtId = model.AtraccionId
            };
        }
        public static void UpdateEntity(Horario entity, HorarioUpdateModel model)
        {
            entity.AtId = model.AtraccionId;
            entity.HorFecha = DateTime.ParseExact(model.Fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            entity.HorHoraInicio = TimeSpan.Parse(model.HoraInicio, CultureInfo.InvariantCulture);

            entity.HorHoraFin = model.HoraFin != null
                ? TimeSpan.Parse(model.HoraFin, CultureInfo.InvariantCulture)
                : (TimeSpan?)null;
            entity.HorCuposDisponibles = model.Cupos;
        }
    }
}
