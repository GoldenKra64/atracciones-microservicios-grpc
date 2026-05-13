using Atraccion.Microservicios.Atraccion.Business.DTOs.Categoria;
using Atraccion.Microservicios.Atraccion.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Validators
{
    public static class CategoriaValidator
    {
        public static void ValidateCreate(CreateCategoriaRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new ValidationException("Nombre obligatorio");
        }

        public static void ValidateUpdate(UpdateCategoriaRequest request)
        {
            if (request.Id <= 0)
                throw new ValidationException("Id inválido");

            ValidateCreate(request);
        }
    }
}
