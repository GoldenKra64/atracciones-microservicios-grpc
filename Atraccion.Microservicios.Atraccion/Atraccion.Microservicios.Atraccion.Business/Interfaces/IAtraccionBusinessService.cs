using Atraccion.Microservicios.Atraccion.Business.DTOs;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion;
using Atraccion.Microservicios.Atraccion.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Interfaces
{
    public interface IAtraccionBusinessService
    {
        Task<AtraccionDetalleDto> GetByIdAsync(string id);

        Task<PagedResponse<ListadoAtracciones>> GetPagedAsync(
            FiltroDto filtro);

        Task<List<AtraccionTypeResponse>> GetAtraccionType();
        Task<AtraccionResponse> GetInternalById(string id);
        Task<List<AtraccionResponse>> GetAllInternalAsync();
        Task CreateAsync(CreateAtraccionRequest request);

        Task UpdateAsync(UpdateAtraccionRequest request);

        Task LogicalDeleteAsync(string id);
    }
}
