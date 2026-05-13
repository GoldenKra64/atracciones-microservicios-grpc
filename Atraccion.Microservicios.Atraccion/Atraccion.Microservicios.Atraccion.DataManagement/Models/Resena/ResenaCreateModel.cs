using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Models.Resena
{
    public class ResenaCreateModel
    {
        public int? ClienteId { get; set; }
        public int AtraccionId { get; set; }
        public string AtraccionGuid { get; set; }
        public int Calificacion { get; set; }
        public string? Comentario { get; set; }
    }
}
