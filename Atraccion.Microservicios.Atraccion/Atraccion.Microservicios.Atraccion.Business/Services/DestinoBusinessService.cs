using Atraccion.Microservicios.Atraccion.Business.DTOs.Destino;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Atraccion.Microservicios.Atraccion.Business.Mappers;
using Atraccion.Microservicios.Atraccion.Business.Validators;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Services
{
    public class DestinoBusinessService : IDestinoBusinessService
    {
        private readonly IDestinoDataService _dataService;

        public DestinoBusinessService(IDestinoDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<DestinoResponse>> GetAllAsync()
        {
            var data = await _dataService.GetAllAsync();
            return data.Select(DestinoBusinessMapper.ToResponse);
        }
        public async Task<DestinoResponse> GetByIdAsync(int id)
        {
            var data = await _dataService.GetByIdAsync(id);
            return DestinoBusinessMapper.ToResponse(data);
        }

        public async Task<int> CreateAsync(CreateDestinoRequest request)
        {
            DestinoValidator.ValidateCreate(request);
            var model = DestinoBusinessMapper.ToCreateModel(request);
            return await _dataService.CreateAsync(model);
        }

        public async Task UpdateAsync(UpdateDestinoRequest request)
        {
            DestinoValidator.ValidateUpdate(request);
            var model = DestinoBusinessMapper.ToUpdateModel(request);
            await _dataService.UpdateAsync(model);
        }

        public async Task LogicalDeleteAsync(int id)
        {
            await _dataService.SoftDeleteAsync(id);
        }
    }
}
