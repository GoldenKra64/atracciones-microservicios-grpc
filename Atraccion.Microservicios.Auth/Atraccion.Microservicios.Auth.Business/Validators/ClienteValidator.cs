using Atraccion.Microservicios.Auth.Business.DTOs.Usuario;
using Atraccion.Microservicios.Auth.Business.Exceptions;
using Atraccion.Microservicios.Auth.DataManagement.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.Business.Validators
{
    public static class ClienteValidator
    {
        public static void ValidateCreate(CreateClienteDto request)
        {
            var errors = new Dictionary<string, string[]>();

            // Nombre (solo dígitos)
            if (string.IsNullOrWhiteSpace(request.Nombres))
            {
                errors["Nombres"] = new[] { "El nombre del cliente no puede ir vacio" };
            }

            // Apellidos (solo dígitos)
            if (string.IsNullOrWhiteSpace(request.Apellidos))
            {
                errors["Apellidos"] = new[] { "El apellido no puede ir vacio" };
            }


            // Correo
            if (string.IsNullOrWhiteSpace(request.Correo))
            {
                errors["Correo"] = new[] { "Correo obligatorio" };
            }
            else if (!request.Correo.Contains("@"))
            {
                errors["Correo"] = new[] { "Correo inválido" };
            }

            // Identificación
            if (string.IsNullOrWhiteSpace(request.NumeroIdentificacion))
            {
                errors["Identificacion"] = new[] { "Obligatorio" };
            }

            // Teléfono (solo dígitos)
            if (!request.Telefono.All(char.IsDigit))
            {
                errors["Telefono"] = new[] { "El teléfono solo debe contener números" };
            }

            if (request.Telefono.Length != 10)
            {
                errors["Telefono"] = new[] { "El teléfono debe de tener 10 caracteres de longitud" };
            }

            // CEDULA
            if (string.IsNullOrWhiteSpace(request.NumeroIdentificacion))
            {
                errors["Numero Identificacion"] = new[] { "El numero de identificación no puede ir vacio" };
            }
            if (!request.NumeroIdentificacion.All(char.IsDigit))
            {
                errors["Numero Identificacion"] = new[] { "El numero de identificacion solo debe contener números" };
            }

            // Tipo de identificación válido
            var tiposValidos = new[] { "CEDULA", "RUC", "PASAPORTE" };

            if (string.IsNullOrWhiteSpace(request.TipoIdentificacion) ||
                !tiposValidos.Contains(request.TipoIdentificacion.ToUpper()))
            {
                errors["TipoIdentificacion"] = new[]
                {
                    "Solo puede ser: CEDULA, RUC o PASAPORTE"
                };
            }

            bool esCedula = request.TipoIdentificacion == "CEDULA" && request.NumeroIdentificacion.Length == 10;
            bool esRuc = request.TipoIdentificacion == "RUC" && request.NumeroIdentificacion.Length == 13;
            bool esPasaporte = request.TipoIdentificacion == "PASAPORTE" && request.NumeroIdentificacion.Length == 13;

            if (!(esCedula || esRuc || esPasaporte))
            {
                errors["Numero Identificacion"] = new[]
                {
                    "El número de identificación no es válido según el tipo seleccionado"
                };
            }

            if (errors.Any())
                throw new ValidationException(errors);
        }
    }
}
