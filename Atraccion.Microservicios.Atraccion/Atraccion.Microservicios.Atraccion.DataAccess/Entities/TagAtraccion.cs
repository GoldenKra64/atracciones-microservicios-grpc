using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class TagAtraccion
    {
        public int TagId { get; set; }
        public int AtId { get; set; }

        public Tag Tag { get; set; }
        public Atraccion Atraccion { get; set; }
    }
}
