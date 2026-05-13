using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class Idioma
    {
        public int IdId { get; set; }
        public string IdNombre { get; set; } = null!;
        public string IdEstado { get; set; } = null!;
        public ICollection<IdiomaAtraccion> IdiomaAtracciones { get; set; } = new List<IdiomaAtraccion>();
    }
}
