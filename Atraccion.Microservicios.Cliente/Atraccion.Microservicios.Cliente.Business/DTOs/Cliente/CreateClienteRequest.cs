using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.Business.DTOs.Cliente
{
    public class CreateClienteRequest
    {
        public int? UsuarioId { get; set; }

        public string TipoIdentificacion { get; set; } = null!;
        public string NumeroIdentificacion { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; }

        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
    }
}
