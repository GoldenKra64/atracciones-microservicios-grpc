using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Models.Resena
{
    public class ResenaUpdateModel
    {
        public int Id { get; set; }

        public int Calificacion { get; set; }
        public string? Comentario { get; set; }
    }
}
