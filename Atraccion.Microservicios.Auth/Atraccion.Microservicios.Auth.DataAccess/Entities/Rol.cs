using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataAccess.Entities
{
    public class Rol
    {
        public int RolId { get; set; }
        public string RolGuid { get; set; }

        public string RolDescripcion { get; set; } = null!;
        public string RolEstado { get; set; } = null!;

        // Relaciones
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}
