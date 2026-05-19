using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion
{
    public class AtraccionesPagedResponseDto
    {
        public IEnumerable<ListadoAtracciones> Items { get; set; } = null!;
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int UnfilteredProductCount { get; set; }
    }
}
