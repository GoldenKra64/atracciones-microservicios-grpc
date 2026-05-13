using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.Business.DTOs.Usuario
{
    public class ChangePasswordRequest
    {
        public int UsuarioId { get; set; }

        public string PasswordActual { get; set; } = null!;
        public string PasswordNuevo { get; set; } = null!;
    }
}
