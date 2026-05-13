using Atraccion.Microservicios.Atraccion.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces
{
    public interface IAtraccionQuery
    {
        Task<PagedResult<Atraccion.DataAccess.Entities.Atraccion>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string? ciudad,
            string? idioma,
            string? ordenarPor,
            decimal? calificacionMin,
            string? horario,
            string? tipo,
            string? subTipo);

        Task<Atraccion.DataAccess.Entities.Atraccion?> GetByIdAsync(string id); // Guid
        Task<List<Atraccion.DataAccess.Entities.Atraccion?>> GetAtraccionTypeAsync();
        Task<Atraccion.DataAccess.Entities.Atraccion?> GetInternalByIdAsync(string id);
        Task<List<Atraccion.DataAccess.Entities.Atraccion?>> GetAllInternalAsync();
    }
}
