using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.DataAccess.Common
{
    public static class StatusChange
    {
        public static void SetEstado(object entity, string value)
        {
            var prop = entity.GetType()
                .GetProperties()
                .FirstOrDefault(p => p.Name.EndsWith("Estado"));

            prop?.SetValue(entity, value);
        }
    }
}
