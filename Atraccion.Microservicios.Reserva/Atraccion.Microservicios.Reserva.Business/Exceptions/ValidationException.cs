using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.Business.Exceptions
{
    public class ValidationException : BusinessException
    {
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException(string message)
            : base(message, "VALIDATION_ERROR")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(Dictionary<string, string[]> errors)
        : base("Uno o más errores de validación ocurrieron.")
        {
            Errors = errors;
        }
    }
}
