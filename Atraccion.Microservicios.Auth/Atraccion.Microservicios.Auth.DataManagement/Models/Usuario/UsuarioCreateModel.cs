using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataManagement.Models.Usuario
{
    public class UsuarioCreateModel
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<int> RolIds { get; set; } = new List<int>();
    }
}
