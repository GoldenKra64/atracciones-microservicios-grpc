using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace Atraccion.Microservicios.Reserva.Business.DTOs
{
    public class BaseResponse
    {
        [JsonIgnore]
        public int Id { get; set; }
        
        [JsonIgnore]
        public string Guid { get; set; }
    }
}
