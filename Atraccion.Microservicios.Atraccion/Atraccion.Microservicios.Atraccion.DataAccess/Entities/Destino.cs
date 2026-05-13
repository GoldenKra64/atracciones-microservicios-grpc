using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class Destino
    {
        public int DesId { get; set; }
        public string DesGuid { get; set; }

        public string DesNombre { get; set; } = null!;
        public string DesPais { get; set; } = null!;
        public string? DesImagenUrl { get; set; }

        public DateTime DesFechaIngreso { get; set; }
        public string DesUsuarioIngreso { get; set; } = null!;
        public string DesIpIngreso { get; set; } = null!;

        public DateTime? DesFechaMod { get; set; }
        public string? DesUsuarioMod { get; set; }
        public string? DesIpMod { get; set; }

        public DateTime? DesFechaEliminacion { get; set; }
        public string? DesUsuarioEliminacion { get; set; }
        public string? DesIpEliminacion { get; set; }

        public string DesEstado { get; set; } = null!;

        // Relaciones
        public ICollection<Atraccion> Atracciones { get; set; } = new List<Atraccion>();
    }
}
