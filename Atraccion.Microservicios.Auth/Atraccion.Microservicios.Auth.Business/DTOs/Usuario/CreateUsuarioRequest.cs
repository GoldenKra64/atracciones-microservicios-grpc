using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.Business.DTOs.Usuario
{
    public class CreateUsuarioRequest
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<int> RolIds { get; set; } = new();
        public CreateClienteDto Cliente { get; set; } = null!;
    }

    public class CreateClienteDto
    {
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
    }
}
