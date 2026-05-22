using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket
{
    public class TicketDto
    {
        [JsonPropertyName("horId")]
        public int? HorId { get; set; }
        
        [JsonPropertyName("hor_guid")]
        public string? HorarioGuid { get; set; }
        
        [JsonPropertyName("tck_guid")]
        public string TckGuid { get; set; }
        
        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }
        
        [JsonPropertyName("precio")]
        public decimal Precio { get; set; }
        
        [JsonPropertyName("moneda")]
        public string Moneda { get; set; } = "USD";
    }
}
