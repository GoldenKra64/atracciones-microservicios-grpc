using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Models.Reserva
{
    public class DetalleReservaModel
    {
        public string tck_guid { get; set; }
        public string tck_tipo_participante { get; set; }
        public int cantidad { get; set; }
        public double precio_unit { get; set; }
        public double subtotal { get; set; }
    }
}
