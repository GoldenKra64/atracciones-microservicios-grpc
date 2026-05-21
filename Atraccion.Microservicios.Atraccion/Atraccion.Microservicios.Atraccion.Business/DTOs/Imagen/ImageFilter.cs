using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Imagen
{
    public class ImageFilter
    {
        [System.Text.Json.Serialization.JsonPropertyName("url")]
        public string Uri { get; set; } = null;
    }
}
