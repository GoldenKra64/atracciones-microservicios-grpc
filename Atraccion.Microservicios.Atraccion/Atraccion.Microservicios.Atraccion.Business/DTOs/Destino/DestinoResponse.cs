using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Destino
{
    public class DestinoResponse : BaseResponse
    {
        public string Nombre { get; set; } = null!;
        public string Pais { get; set; } = null!;
    }
}
