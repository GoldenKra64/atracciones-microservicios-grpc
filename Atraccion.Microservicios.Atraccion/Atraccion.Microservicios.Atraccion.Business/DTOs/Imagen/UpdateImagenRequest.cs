using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Imagen
{
    public class UpdateImagenRequest : CreateImagenRequest
    {
        public int Id { get; set; }
    }
}
