using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataAccess.Entities
{
    public class Usuario
    {
        public int UsuId { get; set; }
        public string UsuGuid { get; set; }

        public string UsuLogin { get; set; } = null!;
        public string UsuPasswordHash { get; set; } = null!;

        public DateTime UsuFechaRegistro { get; set; }
        public string UsuUsuarioRegistro { get; set; } = null!;
        public string UsuIpRegistro { get; set; } = null!;

        public DateTime? UsuFechaMod { get; set; }
        public string? UsuUsuarioMod { get; set; }
        public string? UsuIpMod { get; set; }

        public DateTime? UsuFechaEliminacion { get; set; }
        public string? UsuUsuarioEliminacion { get; set; }
        public string? UsuIpEliminacion { get; set; }

        public string UsuEstado { get; set; } = null!;

        // Relaciones
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}
