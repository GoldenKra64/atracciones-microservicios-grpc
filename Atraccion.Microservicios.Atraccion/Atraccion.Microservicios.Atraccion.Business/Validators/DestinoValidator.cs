using Atraccion.Microservicios.Atraccion.Business.DTOs.Destino;
using Atraccion.Microservicios.Atraccion.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Validators
{
    public static class DestinoValidator
    {
        public static void ValidateCreate(CreateDestinoRequest request)
        {
            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(request.Nombre))
                errors["Nombres"] = new[] { "El nombre del destino no puede ser nulo" };

            if (string.IsNullOrWhiteSpace(request.Pais))
                errors["Pais"] = new[] { "El nombre del país no puede ser nulo" };

            if (errors.Any())
                throw new ValidationException(errors);
        }

        public static void ValidateUpdate(UpdateDestinoRequest request)
        {
            if (request.Id <= 0)
                throw new ValidationException("Id inválido");

            ValidateCreate(request);
        }
    }
}
