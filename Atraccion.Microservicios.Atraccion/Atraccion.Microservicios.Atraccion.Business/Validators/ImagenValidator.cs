using Atraccion.Microservicios.Atraccion.Business.DTOs.Imagen;
using Atraccion.Microservicios.Atraccion.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Validators
{
    public static class ImagenValidator
    {
        public static void ValidateCreate(CreateImagenRequest request)
        {
            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(request.Url))
                errors["URL"] = new[] { "La URL no puede ser nula" };

            if (Uri.TryCreate(request.Url, UriKind.Absolute, out var uriResult))
            {
                // IGNORE
            }
            else
            {
                errors["URL"] = new[] { "Debe especificarse una URL" };
            }

            if (errors.Any())
                throw new ValidationException(errors);
        }

        public static void ValidateUpdate(UpdateImagenRequest request)
        {
            if (request.Id < 0)
            {
                throw new ValidationException("ID inválida");
            }

            ValidateCreate(request);
        }
    }
}
