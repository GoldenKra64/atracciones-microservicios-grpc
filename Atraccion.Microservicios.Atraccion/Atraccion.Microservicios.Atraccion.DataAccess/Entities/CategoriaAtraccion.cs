using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class CategoriaAtraccion
    {
        public int CatId { get; set; }
        public int AtId { get; set; }

        public Categoria Categoria { get; set; } = null!;
        public Atraccion Atraccion { get; set; } = null!;
    }
}
