using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataManagement.Models.Cliente
{
    public class ClienteCreateModel
    {
        public int? UsuarioId { get; set; }

        public string TipoIdentificacion { get; set; } = null!;
        public string NumeroIdentificacion { get; set; } = null!;
        public string Correo { get; set; } = null!;

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
    }
}
