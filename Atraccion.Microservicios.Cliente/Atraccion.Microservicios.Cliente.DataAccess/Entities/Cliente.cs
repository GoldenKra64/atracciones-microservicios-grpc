using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataAccess.Entities
{
    public class Cliente
    {
        public int CliId { get; set; }
        public string CliGuid { get; set; }

        public int? UsuId { get; set; }

        public string CliTipoIdentificacion { get; set; } = null!;
        public string CliNumeroIdentificacion { get; set; } = null!;
        public string CliNombres { get; set; }
        public string CliApellidos { get; set; }
        public string CliCorreo { get; set; } = null!;
        public string? CliTelefono { get; set; }
        public string? CliDireccion { get; set; }

        public DateTime CliFechaIngreso { get; set; }
        public string CliUsuarioIngreso { get; set; } = null!;
        public string CliIpIngreso { get; set; } = null!;

        public DateTime? CliFechaEliminacion { get; set; }
        public string? CliUsuarioEliminacion { get; set; }
        public string? CliIpEliminacion { get; set; }

        public string CliEstado { get; set; } = null!;

        // Navegación
        // public Usuario Usuario { get; set; } = null!;
        // public ICollection<Reserva> Reservas { get; set; }
        // public ICollection<Resena> Resenas { get; set; }
    }
}
