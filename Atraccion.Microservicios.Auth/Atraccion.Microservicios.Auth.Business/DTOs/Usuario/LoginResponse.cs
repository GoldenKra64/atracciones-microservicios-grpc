using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.Business.DTOs.Usuario
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }

        public string? Username { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
