using Atraccion.Microservicios.Auth.Business.DTOs.Usuario;
using Atraccion.Microservicios.Auth.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.Business.Validators
{
    public static class UsuarioValidator
    {
        public static void ValidateCreate(CreateUsuarioRequest request)
        {
            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(request.Login))
                errors["Login"] = new[] { "Login obligatorio" };

            if (request.Password.Length < 6)
                errors["Password"] = new[] { "Mínimo 6 caracteres" };

            if (!request.RolIds.Any())
                errors["Roles"] = new[] { "Debe tener al menos un rol" };

            if (errors.Any())
                throw new ValidationException(errors);
        }

        public static void ValidateUpdate(UpdateUsuarioRequest request)
        {
            if (request.Id <= 0)
                throw new ValidationException("Id inválido");

            if (!request.RolIds.Any())
                throw new ValidationException("Debe tener al menos un rol");
        }
    }
}
