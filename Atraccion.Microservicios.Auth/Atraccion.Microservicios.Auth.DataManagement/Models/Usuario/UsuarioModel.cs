using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataManagement.Models.Usuario
{
    public class UsuarioModel : BaseModel
    {
        public string Login { get; set; } = null!;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
