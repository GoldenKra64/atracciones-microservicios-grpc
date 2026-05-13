using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataAccess.Entities
{
    public class UsuarioRol
    {
        public int UsuRolId { get; set; }

        public int UsuId { get; set; }
        public int RolId { get; set; }

        // Navegación
        public Usuario Usuario { get; set; } = null!;
        public Rol Rol { get; set; } = null!;
    }
}
