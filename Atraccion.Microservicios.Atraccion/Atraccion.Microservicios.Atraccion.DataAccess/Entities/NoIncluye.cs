using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class NoIncluye
    {
        public int NoIncId { get; set; }

        public string NoIncDescripcion { get; set; } = null!;

        public string NoIncEstado { get; set; } = null!;

        public ICollection<NoIncluyeAtraccion> NoIncluyeAtracciones { get; set; } = new List<NoIncluyeAtraccion>();
    }
}
