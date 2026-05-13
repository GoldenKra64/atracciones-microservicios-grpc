using Atraccion.Microservicios.Reserva.Business.DTOs.Reserva;
using Atraccion.Microservicios.Reserva.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.Business.Validators
{
    public static class ReservaValidator
    {
        public static void ValidateCreate(CreateReservaRequest request)
        {
            var errors = new Dictionary<string, string[]>();

            if (request.Lineas == null || !request.Lineas.Any())
                errors["Lineas"] = new[] { "Debe incluir al menos un detalle" };

            foreach (var d in request.Lineas)
            {
                if (d.cantidad <= 0)
                    errors["Cantidad"] = new[] { "Cantidad debe ser mayor a 0" };
            }


            if (errors.Any())
                throw new ValidationException(errors);
        }

        public static void ValidateUpdate(UpdateReservaRequest request)
        {
            if (request.Id == null)
                throw new ValidationException("Id inválido");

            ValidateCreate(request);
        }
    }
}
