using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion
{
    public class ListadoAtracciones
    {
        public string id { get; set; } = null!; // Guid
        public string nombre { get; set; } = null!;
        public string ciudad { get; set; } = null!;
        public string pais { get; set; } = null!;
        public string tipo_tagname { get; set; } = null!;
        public string tipo_nombre { get; set; } = null!;
        public string? subtipo_tagname { get; set; }
        public string? subtipo_nombre { get; set; }
        public List<string> etiquetas { get; set; } = new();
        public string? descripcion_corta { get; set; }
        public string? imagen_principal { get; set; }
        public int duracion_minutos { get; set; }
        public decimal? precio_desde { get; set; }
        public string moneda { get; set; } = "USD";
        public double calificacion { get; set; }
        public int total_resenas { get; set; }
        public List<string> idiomas_disponibles { get; set; } = new();
        public DisponibilidadDto disponibilidad { get; set; } = null!;

        [System.Text.Json.Serialization.JsonPropertyName("_links")]
        public Dictionary<string, string> _links { get; set; } = new();
    }
}
