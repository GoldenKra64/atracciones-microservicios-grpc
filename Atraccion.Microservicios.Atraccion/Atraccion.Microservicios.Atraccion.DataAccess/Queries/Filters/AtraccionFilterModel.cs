using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Queries.Filters
{
    public class AtraccionFilterModel
    {
        public string? Nombre { get; set; }

        public int? DestinoId { get; set; }

        public List<int>? CategoriaIds { get; set; }
        public List<int>? IdiomaIds { get; set; }

        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }

        public bool? IncluyeTransporte { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
