using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.Business.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message)
            : base(message, "NOT_FOUND")
        {
        }

        public NotFoundException(string entityName, object key)
            : base($"{entityName} con id '{key}' no fue encontrado.", "NOT_FOUND")
        {
        }
    }
}
