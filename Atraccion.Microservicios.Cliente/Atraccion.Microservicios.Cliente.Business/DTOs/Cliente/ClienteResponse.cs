using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.Business.DTOs.Cliente
{
    public class ClienteResponse : BaseResponse
    {
        public string NumeroIdentificacion { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
    }
}
