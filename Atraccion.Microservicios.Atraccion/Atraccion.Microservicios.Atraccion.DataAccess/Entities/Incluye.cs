using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class Incluye
    {
        public int IncId { get; set; }

        public string IncDescripcion { get; set; } = null!;

        public string IncEstado { get; set; } = null!;

        public ICollection<IncluyeAtraccion> IncluyeAtracciones { get; set; } = new List<IncluyeAtraccion>();
    }
}
