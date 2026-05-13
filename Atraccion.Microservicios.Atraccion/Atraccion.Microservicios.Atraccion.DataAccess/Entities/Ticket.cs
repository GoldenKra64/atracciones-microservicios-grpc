using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class Ticket
    {
        public int TicId { get; set; }
        public string TicGuid { get; set; }
        public int HorId { get; set; }
        public string TicTitulo { get; set; }
        public decimal TicPrecio { get; set; }
        public string TicTipoParticipante { get; set; }

        // Auditoría ingreso
        public DateTime TicFechaIngreso { get; set; }
        public string TicUsuarioIngreso { get; set; }
        public string TicIpIngreso { get; set; }

        // Auditoría modificación
        public DateTime? TicFechaMod { get; set; }
        public string? TicUsuarioMod { get; set; }
        public string? TicIpMod { get; set; }

        // Eliminación lógica
        public DateTime? TicFechaEliminacion { get; set; }
        public string? TicUsuarioEliminacion { get; set; }
        public string? TicIpEliminacion { get; set; }
        public string TicEstado { get; set; }

        // 🔗 Relación con Horario
        public Horario Horario { get; set; }
        /*public ICollection<DetalleReserva> DetalleReserva { get; set; } = new List<DetalleReserva>();*/
    }
}
