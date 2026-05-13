using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Models.Imagen
{
    public class ImagenModel
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int AtraccionId { get; set; }
    }
}
