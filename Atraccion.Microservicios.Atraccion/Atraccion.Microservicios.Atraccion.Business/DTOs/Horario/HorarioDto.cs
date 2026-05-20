using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Horario
{
    public class HorarioDto
    {
        [JsonIgnore]
        public int HorarioId { get; set; }
        
        [JsonPropertyName("hor_guid")]
        public string HorarioGuid { get; set; }
        
        [JsonIgnore]
        public int AtraccionId { get; set; }
        
        [JsonPropertyName("fecha")]
        public string Fecha { get; set; }
        
        [JsonPropertyName("hora_inicio")]
        public string HoraInicio { get; set; }
        
        [JsonPropertyName("hora_fin")]
        public string? HoraFin { get; set; }
        
        [JsonPropertyName("cupos")]
        public int Cupos { get; set; }
    }
}
