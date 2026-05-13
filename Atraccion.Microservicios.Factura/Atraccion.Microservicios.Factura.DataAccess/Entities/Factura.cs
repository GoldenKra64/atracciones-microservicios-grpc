using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.DataAccess.Entities
{
    public class Factura
    {
        public int FacId { get; set; }
        public string FacGuid { get; set; }

        public int RevId { get; set; }
        public int CliId { get; set; }

        public string FacNumero { get; set; } = null!;
        public DateTime FacFechaEmision { get; set; }
        public decimal FacTotal { get; set; }

        public string? FacObservacion { get; set; }
        public string? FacOrigenCanal { get; set; }

        public string FacEstado { get; set; } = null!;

        public string? FacUsuarioIngreso { get; set; }
        public string? FacIpIngreso { get; set; }

        public DateTime? FacFechaMod { get; set; }
        public string? FacUsuarioMod { get; set; }
        public string? FacIpMod { get; set; }

        public DateTime? FacFechaEliminacion { get; set; }
        public string? FacUsuarioEliminacion { get; set; }
        public string? FacIpEliminacion { get; set; }

        // Relaciones
        // public Reserva Reserva { get; set; } = null!;
    }
}
