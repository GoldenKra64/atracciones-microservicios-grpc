using Atraccion.Microservicios.Reserva.DataManagement.Models;
using Atraccion.Microservicios.Reserva.DataManagement.Models.Reserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Interfaces
{
    public interface IReservaDataService
    {
        Task<ReservaModel> CreateAsync(ReservaCreateModel model, bool isPublic = false);
        Task<ReservaModel?> GetByIdAsync(string id);
        Task<List<ReservaModel?>> GetAllAsync();

        Task<DataPagedResult<ReservaModel>> GetByClienteAsync(int clienteId, int page, int size);
        Task<ReservaModel?> UpdateAsync(UpdateReservaModel model);
        Task SoftDeleteAsync(string id);
        Task CancelAsync(string id);
        Task ApproveAsync(string id, string? nombreReceptor, string? correoReceptor);
    }
}
