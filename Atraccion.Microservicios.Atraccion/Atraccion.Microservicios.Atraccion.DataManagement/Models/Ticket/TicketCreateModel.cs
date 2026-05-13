using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Models.Ticket
{
    public class TicketCreateModel
    {
        public int HorarioId { get; set; }

        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Tipo { get; set; }
    }
}
