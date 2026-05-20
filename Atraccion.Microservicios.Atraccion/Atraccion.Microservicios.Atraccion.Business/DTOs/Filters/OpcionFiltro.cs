using Atraccion.Microservicios.Atraccion.Business.DTOs.Imagen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Atraccion.Microservicios.Atraccion.Business.DTOs.Imagen;
using System.Text.Json.Serialization;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Filters
{
    public class OpcionFiltro
    {
        [JsonPropertyName("name")]
        public string nombre { get; set; }
        public string tagname { get; set; }
        public int productCount { get; set; }
        public ImageFilter? image { get; set; }
        
        [JsonPropertyName("childFilterOptions")]
        public List<OpcionFiltro>? childFilterOptions { get; set; }
    }
}
