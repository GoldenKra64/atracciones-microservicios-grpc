using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Resena
{
    public class CreateResenaRequest
    {
        public int? ClienteId { get; set; }
        public string AtraccionGuid { get; set; }
        public int Calificacion { get; set; }
        public string? Comentario { get; set; }
    }
}
