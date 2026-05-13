using Atraccion.Microservicios.Atraccion.DataManagement.Models.Categoria;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Destino;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Horario;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Imagen;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Incluye;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.NoIncluye;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Resena;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Models.Atraccion
{
    public class AtraccionModel : BaseModel
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string Direccion { get; set; }
        public int? DuracionMinutos { get; set; }
        public decimal? PrecioReferencia { get; set; }

        public bool IncluyeTransporte { get; set; }
        public bool IncluyeAcompaniante { get; set; }

        public string? PuntoEncuentro { get; set; }
        public string? Moneda { get; set; }

        // Relaciones
        public DestinoModel Destino { get; set; } = null!;
        public List<ImagenModel> Imagenes { get; set; } = new();
        public List<CategoriaModel> Categorias { get; set; } = new();
        public List<IdiomaModel> Idiomas { get; set; } = new();
        public List<IncluyeModel> Incluyes { get; set; } = new();
        public List<NoIncluyeModel> NoIncluyes { get; set; } = new();
        public List<TagModel> TagAtracciones { get; set; } = new();
        public List<HorarioModel> Horarios { get; set; } = new();
        public List<ResenaModel> Resena { get; set; } = new();
    }
}
