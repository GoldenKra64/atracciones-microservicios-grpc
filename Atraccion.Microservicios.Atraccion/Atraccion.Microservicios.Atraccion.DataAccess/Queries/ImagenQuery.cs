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
    public class ImagenQuery : IImagenQuery
    {
        private readonly AtraccionesDbContext _context;
        public ImagenQuery(AtraccionesDbContext context)
        {
            _context = context;
        }
        public async Task<List<Imagen?>> GetAllAsync()
        {
            return await _context.Imagenes.Where(c => c.ImgEstado == "ACT").ToListAsync();
        }
    }
}
