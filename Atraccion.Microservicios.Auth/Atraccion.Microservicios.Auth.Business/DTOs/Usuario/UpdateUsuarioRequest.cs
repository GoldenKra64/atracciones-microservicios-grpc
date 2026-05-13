using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.Business.DTOs.Usuario
{
    public class UpdateUsuarioRequest
    {
        public int Id { get; set; }

        public string Login { get; set; } = null!;

        public List<int> RolIds { get; set; } = new();
    }
}
