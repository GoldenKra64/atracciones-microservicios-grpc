using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket
{
    public class TicketRes : BaseResponse
    {
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Tipo { get; set; }
        public int HorarioId { get; set; }
        
        [System.Text.Json.Serialization.JsonPropertyName("hor_guid")]
        public string? HorarioGuid { get; set; }
    }
}
