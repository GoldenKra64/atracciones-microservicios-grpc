using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.Business.DTOs
{
    public class ClienteInvitadoRequest
    {
        public string tipo_identificacion { get; set; }
        public string numero_identificacion { get; set; }
        public string? nombres { get; set; }
        public string? apellidos { get; set; }
        public string? razon_social { get; set; }
        public string correo { get; set; }
        public string? telefono { get; set; }
        public string? direccion { get; set; }
    }
}
