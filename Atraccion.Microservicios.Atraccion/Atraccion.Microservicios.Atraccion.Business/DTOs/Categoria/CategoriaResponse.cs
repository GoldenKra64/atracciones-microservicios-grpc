using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Categoria
{
    public class CategoriaResponse : BaseResponse
    {
        public string Nombre { get; set; } = null!;
        public List<CategoriaResponse> Children { get; set; } = new();
    }
}
