using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Models.Categoria
{
    public class CategoriaCreateModel
    {
        public string Nombre { get; set; } = null!;
        public int? ParentId { get; set; }
    }
}
