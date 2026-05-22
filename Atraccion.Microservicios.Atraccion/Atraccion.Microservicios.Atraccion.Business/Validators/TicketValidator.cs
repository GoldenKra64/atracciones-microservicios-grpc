using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using Atraccion.Microservicios.Atraccion.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Validators
{
    public static class TicketValidator
    {
        public static void ValidateCreate(CreateTicketRequest request)
        {
            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(request.HorarioId))
                errors["Horario"] = new[] { "Horario es obligatorio" };

            if (string.IsNullOrWhiteSpace(request.Nombre))
                errors["Nombre"] = new[] { "Obligatorio" };

            if (request.Precio <= 0)
                errors["Precio"] = new[] { "Debe ser mayor a 0" };

            if (string.IsNullOrWhiteSpace(request.Tipo))
                errors["Tipo"] = new[] { "Obligatorio" };

            // Tipo de identificación válido
            var tiposValidos = new[] { "JUNIOR", "SENIOR", "CARETAKER", "ELDER", "YOUNG ADULT" };

            if (string.IsNullOrWhiteSpace(request.Tipo) ||
                !tiposValidos.Contains(request.Tipo.ToUpper()))
            {
                errors["Tipo"] = new[]
                {
                    "Solo puede ser: JUNIOR, SENIOR, CARETAKER, ELDER, YOUNG ADULT"
                };
            }

            if (errors.Any())
                throw new ValidationException(errors);
        }

        public static void ValidateUpdate(UpdateTicketRequest request)
        {
            if (request.Id <= 0)
            {
                throw new ValidationException("El ID no es valido");
            }
        }
    }
}
