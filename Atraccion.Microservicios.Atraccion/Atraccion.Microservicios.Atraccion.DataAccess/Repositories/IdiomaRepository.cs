using Atraccion.Microservicios.Atraccion.DataAccess.Context;
using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Repositories
{
    public class IdiomaRepository : Repository<Idioma>
    {
        public IdiomaRepository(AtraccionesDbContext context) : base(context) { }

        public override async Task SoftDeleteAsync(int id)
        {
            throw new Exception("No se puede eliminar un idioma");
        }
    }
}
