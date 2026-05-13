using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Destino
{
    public class UpdateDestinoRequest : CreateDestinoRequest
    {
        public int Id { get; set; }
    }
}
