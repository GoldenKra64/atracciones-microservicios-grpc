using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class NoIncluyeAtraccion
    {
        public int NoIncId { get; set; }
        public int AtId { get; set; }

        // Navegación
        public NoIncluye NoIncluye { get; set; } = null!;
        public Atraccion Atraccion { get; set; } = null!;
    }
}
