using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs
{
    public class LinksDto
    {
        [JsonPropertyName("self")]
        public string Self { get; set; }
    }
}
