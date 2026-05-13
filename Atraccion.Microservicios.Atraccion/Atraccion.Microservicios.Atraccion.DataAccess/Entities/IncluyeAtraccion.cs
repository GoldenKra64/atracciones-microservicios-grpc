using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class IncluyeAtraccion
    {
        public int IncId { get; set; }
        public int AtId { get; set; }

        // Navegación
        public Incluye Incluye { get; set; } = null!;
        public Atraccion Atraccion { get; set; } = null!;
    }
}
