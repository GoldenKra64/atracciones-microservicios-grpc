using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Entities
{
    public class Categoria
    {
        public int CatId { get; set; }
        public string CatGuid { get; set; }

        public int? CatParentId { get; set; }
        public string CatNombre { get; set; } = null!;
        public string CatEstado { get; set; } = null!;

        // 🔥 Self reference
        public Categoria? Parent { get; set; }
        public ICollection<Categoria> Children { get; set; } = new List<Categoria>();

        public ICollection<CategoriaAtraccion> CategoriaAtracciones { get; set; } = new List<CategoriaAtraccion>();
    }
}
