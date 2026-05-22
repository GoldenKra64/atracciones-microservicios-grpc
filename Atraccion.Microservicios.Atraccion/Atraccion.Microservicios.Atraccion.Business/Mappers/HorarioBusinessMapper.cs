using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using Atraccion.Microservicios.Atraccion.DataAccess.Common;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Horario;
using Atraccion.Microservicios.Atraccion.DataManagement.Services;
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
                AtraccionGuid = model.AtraccionGuid,
                Fecha = model.Fecha,
                HoraInicio = model.HoraInicio,
                HoraFin = model.HoraFin,
                Cupos = model.Cupos
            };
        }
    }
}
