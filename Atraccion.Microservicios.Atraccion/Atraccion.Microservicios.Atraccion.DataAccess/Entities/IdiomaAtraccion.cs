using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class IdiomaAtraccion
    {
        public int IdId { get; set; }
        public int AtId { get; set; }

        public Idioma Idioma { get; set; } = null!;
        public Atraccion Atraccion { get; set; } = null!;
    }
}
