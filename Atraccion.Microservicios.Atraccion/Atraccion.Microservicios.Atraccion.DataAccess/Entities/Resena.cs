using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class Resena
    {
        public int ResenaId { get; set; }
        public string ResenaGuid { get; set; }

        public int AtId { get; set; }
        public int CliId { get; set; }
        public int? RevId { get; set; }

        public int ResenaCalificacion { get; set; }
        public string? ResenaComentario { get; set; }

        public DateTime ResenaFechaCreacion { get; set; }
        public string ResenaUsuarioCreacion { get; set; } = null!;
        public string ResenaIpCreacion { get; set; } = null!;

        public DateTime? ResenaFechaMod { get; set; }
        public string? ResenaUsuarioMod { get; set; }
        public string? ResenaIpMod { get; set; }

        public DateTime? ResenaFechaEliminacion { get; set; }
        public string? ResenaUsuarioEliminacion { get; set; }
        public string? ResenaIpEliminacion { get; set; }

        public string ResenaEstado { get; set; } = null!;

        // Navegación
        public Atraccion Atraccion { get; set; } = null!;
        
        /*
        public Reserva? Reserva { get; set; }
        public Cliente Cliente { get; set; } = null!;
        */
    }
}
