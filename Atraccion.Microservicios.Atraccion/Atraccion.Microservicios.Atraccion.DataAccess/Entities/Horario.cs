using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class Horario
    {
        public int HorId { get; set; }
        public string HorGuid { get; set; }
        public int AtId { get; set; }
        public DateTime HorFecha { get; set; }
        public TimeSpan HorHoraInicio { get; set; }
        public TimeSpan? HorHoraFin { get; set; }
        public int HorCuposDisponibles { get; set; }
        public DateTime HorFechaIngreso { get; set; }
        public string HorUsuarioIngreso { get; set; }
        public string HorIpIngreso { get; set; }
        public DateTime? HorFechaMod { get; set; }
        public string? HorUsuarioMod { get; set; }
        public string? HorIpMod { get; set; }
        public DateTime? HorFechaEliminacion { get; set; }
        public string? HorUsuarioEliminacion { get; set; }
        public string? HorIpEliminacion { get; set; }
        public string HorEstado { get; set; }


        /* Relaciones */
        public ICollection<Ticket> Ticket { get; set; }
        public Atraccion Atraccion { get; set; }
    }
}
