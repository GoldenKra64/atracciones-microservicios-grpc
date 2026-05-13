using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Models.Categoria
{
    public class CategoriaModel : BaseModel
    {
        public string Nombre { get; set; } = null!;

        public int? ParentId { get; set; }

        public List<CategoriaModel> Children { get; set; } = new();
    }
}
