using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion
{
    public class ListadoAtracciones : DisponibilidadDto
    {
        public string id { get; set; } // Guid
        public string nombre { get; set; } = null!;
        public string ciudad { get; set; } = null!;
        public string pais { get; set; } = null!;
        public string tipo_tagname { get; set; } = null!;
        public string tipo_nombre { get; set; } = null!;
        public string? descripcion_corta { get; set; }
        public decimal? precio_desde { get; set; }
        public string moneda { get; set; } = "USD";
        public double calificacion { get; set; }
        public int total_resenias { get; set; }
        public List<string> idiomas_disponibles { get; set; }

        public int duracion_minutos { get; set; }
        public string? imagen_principal { get; set; }
        public List<string> etiquetas { get; set; } = new();
    }
}
