using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket
{
    public class TicketDto
    {
        public int? HorId { get; set; }
        public string TckGuid { get; set; }
        public string Tipo { get; set; }
        public decimal Precio { get; set; }
        public string Moneda { get; set; } = "USD";
    }
}
