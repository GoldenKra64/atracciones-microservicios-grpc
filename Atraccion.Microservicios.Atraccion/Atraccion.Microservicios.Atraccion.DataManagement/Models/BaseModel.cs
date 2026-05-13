using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Estado { get; set; } = null!;
    }
}
