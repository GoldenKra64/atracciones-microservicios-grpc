using Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion;
using Atraccion.Microservicios.Atraccion.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Validators
{
    public static class AtraccionValidator
    {
        public static void ValidateCreate(CreateAtraccionRequest request)
        {
            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(request.Nombre))
                errors["Nombre"] = new[] { "Obligatorio" };

            if (string.IsNullOrWhiteSpace(request.Direccion))
                errors["Direccion"] = new[] { "Obligatorio" };

            if (request.DestinoId <= 0)
                errors["DestinoId"] = new[] { "Inválido" };

            if (request.PrecioReferencia != null && request.PrecioReferencia < 0)
                errors["Precio"] = new[] { "No puede ser negativo" };

            if (request.DuracionMinutos != null && request.DuracionMinutos < 0)
            {
                errors["Duracion minutos"] = new[] { "No puede ser negativo" };
            }

            if (!request.CategoriaIds.Any())
                errors["Categorias"] = new[] { "Debe tener al menos una categoría" };

            if (request.IdiomaIds != null && request.IdiomaIds.Any(i => i <= 0))
                errors["Idiomas"] = new[] { "Idiomas inválidos" };

            if (request.IncluyeIds != null && request.IncluyeIds.Any(i => i <= 0))
                errors["Incluyes"] = new[] { "Incluyes inválidos" };

            if (request.NoIncluyeIds != null && request.NoIncluyeIds.Any(i => i <= 0))
                errors["No Incluye"] = new[] { "No Incluye ID inválidos" };

            if (request.TagIds != null && request.TagIds.Any(i => i <= 0))
                errors["Tags"] = new[] { "IDs de Etiquetas inválidos" };

            if (errors.Any())
                throw new ValidationException(errors);
        }

        public static void ValidateUpdate(UpdateAtraccionRequest request)
        {
            if (request.Id == null)
                throw new ValidationException("Id inválido");

            ValidateCreate(request);
        }
    }
}
