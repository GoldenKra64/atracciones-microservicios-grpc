using Atraccion.Microservicios.Atraccion.Business.DTOs.Categoria;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Destino;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Idioma;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Incluye;
using Atraccion.Microservicios.Atraccion.Business.DTOs.NoIncluye;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Tag;
using Atraccion.Microservicios.Atraccion.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion
{
    public class AtraccionResponse : BaseModel
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string Direccion { get; set; }
        public int? DuracionMinutos { get; set; }
        public double? PrecioReferencia { get; set; }

        public bool IncluyeTransporte { get; set; }
        public bool IncluyeAcompaniante { get; set; }

        public string? PuntoEncuentro { get; set; }
        public string? Moneda { get; set; }

        // Relaciones
        public DestinoResponse Destino { get; set; } = null!;
        public List<CategoriaResponse> Categorias { get; set; } = new();
        public List<IdiomaResponse> Idiomas { get; set; } = new();
        public List<IncluyeResponse> Incluyes { get; set; } = new();
        public List<NoIncluyeResponse> NoIncluyes { get; set; } = new();
        public List<TagResponse> TagAtracciones { get; set; } = new();
    }
}
