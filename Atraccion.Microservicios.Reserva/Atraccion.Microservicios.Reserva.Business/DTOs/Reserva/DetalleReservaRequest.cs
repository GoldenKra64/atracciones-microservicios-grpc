using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.Business.DTOs.Reserva
{
    public class DetalleReservaRequest
    {
        public string tck_guid { get; set; }
        public int cantidad { get; set; }
    }
}
