using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion
{
    public class UpdateAtraccionRequest : CreateAtraccionRequest
    {
        public string? Id { get; set; }
    }
}
