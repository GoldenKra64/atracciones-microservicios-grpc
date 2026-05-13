using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket
{
    public class UpdateTicketRequest : CreateTicketRequest
    {
        public int Id { get; set; }
    }
}
