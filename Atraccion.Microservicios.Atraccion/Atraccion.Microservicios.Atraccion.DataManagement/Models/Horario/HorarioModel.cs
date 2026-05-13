using Atraccion.Microservicios.Atraccion.DataManagement.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Models.Horario
{
    public class HorarioModel
    {
        public int HorarioId { get; set; }
        public string HorarioGuid { get; set; }
        public int AtraccionId { get; set; }
        public string Fecha { get; set; }           // "yyyy-MM-dd"
        public string HoraInicio { get; set; }      // "HH:mm"
        public string? HoraFin { get; set; }        // "HH:mm"
        public int Cupos { get; set; }
        public List<TicketModel> Tickets { get; set; } = new List<TicketModel>();
    }
}
