using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.Business.DTOs.Reserva
{
    public class CreateReservaRequest
    {
        public int? ClienteId { get; set; }
        public string hor_guid { get; set; } = null!;
        public string origen_canal { get; set; } = null!;
        public List<DetalleReservaRequest> Lineas { get; set; } = new();
    }
}
