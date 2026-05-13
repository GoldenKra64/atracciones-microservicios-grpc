using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Resena
{
    public class UpdateResenaRequest : CreateResenaRequest
    {
        public int Id { get; set; }

        public string Estado { get; set; } = null!; // ACT, INA, ELI
    }
}
