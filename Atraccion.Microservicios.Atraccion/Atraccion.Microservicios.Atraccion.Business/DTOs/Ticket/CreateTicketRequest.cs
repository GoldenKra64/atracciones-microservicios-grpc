using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket
{
    public class CreateTicketRequest
    {
        public int HorarioId { get; set; }

        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Tipo { get; set; }
    }
}
