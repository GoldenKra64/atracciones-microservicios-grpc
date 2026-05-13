using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.Business.DTOs.Reserva
{
    public class UpdateReservaRequest : CreateReservaRequest
    {
        public string? Id { get; set; }
    }
}
