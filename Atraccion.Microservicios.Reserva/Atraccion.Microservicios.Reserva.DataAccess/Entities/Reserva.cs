using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataAccess.Entities
{
    public class Reserva
    {
        public int RevId { get; set; }
        public string RevGuid { get; set; }

        public string RevCodigo { get; set; } = null!;

        public int? CliId { get; set; }

        public DateTime RevFechaReservaUtc { get; set; }

        public double RevSubtotal { get; set; }
        public double RevValorIva { get; set; }
        public double RevTotal { get; set; }
        public string RevCanal { get; set; } = null!;

        public int? HorId { get; set; }
        public string? HorGuid { get; set; }
        public string? HorFecha { get; set; }
        public string? HorHoraInicio { get; set; }
        public string? HorHoraFin { get; set; }

        public string? AtNombre { get; set; }

        public string RevUsuarioIngreso { get; set; } = null!;
        public string RevIpIngreso { get; set; } = null!;

        public DateTime? RevFechaMod { get; set; }
        public string? RevUsuarioMod { get; set; }
        public string? RevIpMod { get; set; }

        public DateTime? RevFechaCancelacion { get; set; }
        public string? RevUsuarioCancelacion { get; set; }
        public string? RevIpCancelacion { get; set; }
        public string? RevMotivoCancelacion { get; set; }

        public string RevEstado { get; set; } = null!;

        public ICollection<DetalleReserva> Detalles { get; set; } = new List<DetalleReserva>();
    }
}
