using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class Imagen
    {
        public int ImgId { get; set; }
        public string ImgGuid { get; set; }

        public string ImgUrl { get; set; } = null!;
        public string? ImgDescripcion { get; set; }

        public int AtId { get; set; }

        public DateTime ImgFechaIngreso { get; set; }
        public string ImgUsuarioIngreso { get; set; } = null!;
        public string ImgIpIngreso { get; set; } = null!;

        public DateTime? ImgFechaMod { get; set; }
        public string? ImgUsuarioMod { get; set; }
        public string? ImgIpMod { get; set; }

        public DateTime? ImgFechaEliminacion { get; set; }
        public string? ImgUsuarioEliminacion { get; set; }
        public string? ImgIpEliminacion { get; set; }

        public string ImgEstado { get; set; } = null!;

        // Navegación
        public Atraccion Atraccion { get; set; } = null!;
    }
}
