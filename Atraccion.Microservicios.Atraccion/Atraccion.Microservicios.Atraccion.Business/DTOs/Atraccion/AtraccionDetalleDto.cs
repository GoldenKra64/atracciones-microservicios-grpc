using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion
{
    public class AtraccionDetalleDto
    {
        // 🆔 Identificación
        public string id { get; set; }
        public string nombre { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
        public string tipo_tagname { get; set; }
        public string tipo_nombre { get; set; }
        public string subtipo_tagname { get; set; }
        public string subtipo_nombre { get; set; }
        public List<string> etiquetas { get; set; } = new();
        public string descripcion_corta { get; set; }
        public string imagen_principal { get; set; }
        public int duracion_minutos { get; set; }
        public decimal precio_desde { get; set; }
        public string moneda { get; set; }
        public double calificacion { get; set; }
        public int total_resenas { get; set; }
        public List<string> idiomas_disponibles { get; set; } = new();
        public DisponibilidadDto disponibilidad { get; set; }

        public string descripcion { get; set; }
        public List<string> imagenes { get; set; } = new();
        public List<string> incluye { get; set; } = new();
        public List<string> no_incluye { get; set; } = new();
        public string punto_encuentro { get; set; }
        public Boolean incluye_transporte { get; set; }
        public Boolean incluye_acompaniante { get; set; }
        public List<TicketDto> tickets { get; set; } = new();

        // 🔗 HATEOAS
        [JsonPropertyName("_links")]
        public LinksDto Links { get; set; }
    }
}
