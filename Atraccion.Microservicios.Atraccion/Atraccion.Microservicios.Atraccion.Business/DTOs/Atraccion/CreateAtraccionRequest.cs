using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion
{
    public class CreateAtraccionRequest
    {
        public int DestinoId { get; set; }

        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Direccion { get; set; }
        public int? DuracionMinutos { get; set; }
        public string? PuntoEncuentro { get; set; }
        public string? Moneda { get; set; } = "USD";
        public decimal PrecioReferencia { get; set; }
        public bool IncluyeTransporte { get; set; }
        public bool IncluyeAcompaniante { get; set; }

        public List<int> CategoriaIds { get; set; } = new();
        public List<int> IdiomaIds { get; set; } = new();
        public List<int> IncluyeIds { get; set; } = new();
        public List<int> NoIncluyeIds { get; set; } = new();
        public List<int> TagIds { get; set; } = new();
        // public List<int> ImageIds { get; set; } = new();
        // public List<int> HorarioIds { get; set; } = new();
    }
}
