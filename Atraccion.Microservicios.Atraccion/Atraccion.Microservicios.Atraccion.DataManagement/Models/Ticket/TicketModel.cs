using Atraccion.Microservicios.Atraccion.DataManagement.Models.Horario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Models.Ticket
{
    public class TicketModel : BaseModel
    {
        public string Guid { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Tipo { get; set; } = null!; // Ejemplo: Adulto, Niño, Senior, etc.
        public int HorarioId { get; set; }
        public string? HorarioGuid { get; set; }
        public HorarioModel Horario { get; set; }
    }
}
