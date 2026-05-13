using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Validators
{
    public static class HorarioValidator
    {
        public static void ValidateCreate(CreateHorarioRequest request)
        {
            var errors = new Dictionary<string, string[]>();

            if (request.AtraccionId <= 0)
                errors["Atraccion"] = new[] { "Atraccion invalida" };

            if (request.Cupos < 0)
                errors["Cupos"] = new[] { "Los cupos no pueden ser negativos" };

            if (!DateTime.TryParseExact(
                    request.Fecha,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var fecha))
            {
                errors["Fecha"] = new[] { "La fecha debe de estar parseada de la forma yyyy-mm-dd" };
            }

            if (fecha.Date < DateTime.UtcNow.Date)
                errors["Fecha"] = new[] { "La fecha no puede ser pasada" };

            if (!TimeSpan.TryParseExact(
                    request.HoraInicio,
                    @"hh\:mm",
                    CultureInfo.InvariantCulture,
                    out var horaInicio))
            {
                errors["Hora Inicio"] = new[] { "Formato de hora inválido. Use HH:mm" };
            }

            if (request.HoraFin != null)
            {
                if (!TimeSpan.TryParseExact(
                    request.HoraFin,
                    @"hh\:mm",
                    CultureInfo.InvariantCulture,
                    out var horaFin))
                {
                    errors["Hora Fin"] = new[] { "Formato de hora inválido. Use HH:mm" };
                }
                if (horaFin <= horaInicio)
                    errors["Hora Fin"] = new[] { "Hora fin no puede ser menor a hora inicio" };
            }

            if (string.IsNullOrWhiteSpace(request.HoraInicio))
                errors["Hora Inicio"] = new[] { "Hora inicio no puede estar vacia" };

            if (string.IsNullOrWhiteSpace(request.Fecha))
                errors["Fecha"] = new[] { "Fecha no puede estar vacia" };

            if (errors.Any())
                throw new ValidationException(errors);
        }

        public static void ValidateUpdate(UpdateHorarioRequest request)
        {
            if (request.Id <= 0)
                throw new ValidationException("Id inválido");

            var errors = new Dictionary<string, string[]>();

            if (request.AtraccionId <= 0)
                errors["Atraccion"] = new[] { "Atraccion invalida" };

            if (request.Cupos < 0)
                errors["Cupos"] = new[] { "Los cupos no pueden ser negativos" };

            if (!DateTime.TryParseExact(
                    request.Fecha,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var fecha))
            {
                errors["Fecha"] = new[] { "La fecha debe de estar parseada de la forma yyyy-mm-dd" };
            }

            if (fecha.Date < DateTime.UtcNow.Date)
                errors["Fecha"] = new[] { "La fecha no puede ser pasada" };

            if (!TimeSpan.TryParseExact(
                    request.HoraInicio,
                    @"hh\:mm",
                    CultureInfo.InvariantCulture,
                    out var horaInicio))
            {
                errors["Hora Inicio"] = new[] { "Formato de hora inválido. Use HH:mm" };
            }

            if (request.HoraFin != null)
            {
                if (!TimeSpan.TryParseExact(
                    request.HoraFin,
                    @"hh\:mm",
                    CultureInfo.InvariantCulture,
                    out var horaFin))
                {
                    errors["Hora Fin"] = new[] { "Formato de hora inválido. Use HH:mm" };
                }
                if (horaFin <= horaInicio)
                    errors["Hora Fin"] = new[] { "Hora fin no puede ser menor a hora inicio" };
            }


            if (string.IsNullOrWhiteSpace(request.HoraInicio))
                errors["Hora Inicio"] = new[] { "Hora inicio no puede estar vacia" };

            if (string.IsNullOrWhiteSpace(request.Fecha))
                errors["Fecha"] = new[] { "Fecha no puede estar vacia" };

            if (errors.Any())
                throw new ValidationException(errors);
        }
    }
}
