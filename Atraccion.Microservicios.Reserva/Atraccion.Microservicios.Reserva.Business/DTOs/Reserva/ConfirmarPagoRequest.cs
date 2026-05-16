using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.Business.DTOs.Reserva
{
    public class ConfirmarPagoRequest
    {
        public string nombre_receptor { get; set; } = null!;
        public string apellido_receptor { get; set; } = null!;
        public string correo_receptor { get; set; } = null!;
        public string telefono_receptor { get; set; } = null!;
        public string observacion { get; set; } = null!;
    }
}
