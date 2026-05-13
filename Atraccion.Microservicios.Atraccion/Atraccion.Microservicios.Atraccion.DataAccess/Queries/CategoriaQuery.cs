using Atraccion.Microservicios.Atraccion.DataAccess.Context;
using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Queries
{
    public class CategoriaQuery : ICategoriaQuery
    {
        private readonly AtraccionesDbContext _context;

        public CategoriaQuery(AtraccionesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> GetTreeAsync()
        {
            return await _context.Categorias
                .Include(c => c.Children)
                .Where(c => c.CatParentId == null && c.CatEstado == "ACT")
                .ToListAsync();
        }
    }
}
