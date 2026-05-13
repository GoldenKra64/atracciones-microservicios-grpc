using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Models.Reserva
{
    public class DetalleReservaCreateModel
    {
        public string TicketId { get; set; }
        public int Cantidad { get; set; }
    }
}
