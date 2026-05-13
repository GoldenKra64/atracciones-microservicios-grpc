using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Horario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Mappers
{
    public static class HorarioBusinessMapper
    {
        public static HorarioCreateModel ToCreateModel(CreateHorarioRequest request)
        {
            return new HorarioCreateModel
            {
                AtraccionId = request.AtraccionId,
                Fecha = request.Fecha,
                HoraInicio = request.HoraInicio,
                HoraFin = request.HoraFin,
                Cupos = request.Cupos
            };
        }

        public static HorarioUpdateModel ToUpdateModel(UpdateHorarioRequest request)
        {
            return new HorarioUpdateModel
            {
                Id = request.Id,
                AtraccionId = request.AtraccionId,
                Fecha = request.Fecha,
                HoraInicio = request.HoraInicio,
                HoraFin = request.HoraFin,
                Cupos = request.Cupos
            };
        }

        public static HorarioDto ToResponse(HorarioModel model)
        {
            return new HorarioDto
            {
                HorarioId = model.HorarioId,
                HorarioGuid = model.HorarioGuid,
                AtraccionId = model.AtraccionId,
                Fecha = model.Fecha,
                HoraInicio = model.HoraInicio,
                HoraFin = model.HoraFin,
                Cupos = model.Cupos
            };
        }
    }
}
