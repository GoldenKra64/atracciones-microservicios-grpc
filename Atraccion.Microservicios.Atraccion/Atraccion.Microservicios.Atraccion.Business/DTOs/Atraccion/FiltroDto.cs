using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion
{
    public class FiltroDto
    {
        // 🔍 Filtros principales
        public string? Ciudad { get; set; }
        public string? Tipo { get; set; }        // cat_guid raíz
        public string? Subtipo { get; set; }     // cat_guid hijo
        public string? Etiqueta { get; set; }    // free_cancellation, etc
        public string? Idioma { get; set; }      // es, en, etc

        // ⭐ Rating
        public decimal? CalificacionMin { get; set; }

        // ⏰ Horario (ej: "05:00-12:00")
        public string? Horario { get; set; }

        // 📦 Disponibilidad
        public bool? Disponible { get; set; }

        // 🔃 Ordenamiento
        public string? OrdenarPor { get; set; } = "trending";

        // 📄 Paginación
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}
