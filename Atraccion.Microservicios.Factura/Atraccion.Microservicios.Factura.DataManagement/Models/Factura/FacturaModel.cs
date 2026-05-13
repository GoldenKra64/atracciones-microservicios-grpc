using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.DataManagement.Models.Factura
{
    public class FacturaModel : BaseModel
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Numero { get; set; } = null!;
        public string FechaEmision { get; set; }
        public double Total { get; set; }
        public string OrigenCanal { get; set; } = null!;
        public string? Observacion { get; set; }
        public string Estado { get; set; }
    }
}
