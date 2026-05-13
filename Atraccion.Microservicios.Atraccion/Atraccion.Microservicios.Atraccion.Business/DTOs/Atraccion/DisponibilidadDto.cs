using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion
{
    public class DisponibilidadDto
    {
        public bool disponible { get; set; }
        public bool disponible_hoy { get; set; }
        public string proxima_fecha_disponible { get; set; } = null!;
        public int cupos_disponibles { get; set; }
    }
}
