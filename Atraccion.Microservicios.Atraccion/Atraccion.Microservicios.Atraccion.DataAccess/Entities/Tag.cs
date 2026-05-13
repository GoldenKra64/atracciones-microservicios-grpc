using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagDescription { get; set; }
        public ICollection<TagAtraccion> TagAtracciones { get; set; } = new List<TagAtraccion>();
    }
}
