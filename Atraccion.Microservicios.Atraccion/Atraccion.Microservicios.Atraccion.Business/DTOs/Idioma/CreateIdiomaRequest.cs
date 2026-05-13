using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Idioma
{
    public class CreateIdiomaRequest
    {
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
    }
}
