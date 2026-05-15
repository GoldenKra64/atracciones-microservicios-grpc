using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.Business.DTOs.Factura
{
    public class FacturaResponse : BaseResponse
    {
        public int Id { get; set; }
        public string fac_guid{ get; set; }
        public string fac_numero { get; set; } = null!;
        public int rev_codigo { get; set; }
        public string fecha_emision { get; set; }
        public double total { get; set; }
        public string OrigenCanal { get; set; } = null!;
        public string? Observacion { get; set; }
        public string estado { get; set; }
        public string nombre_receptor { get; set; }
        public string correo_receptor { get; set; }
    }
}
