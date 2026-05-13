using Atraccion.Microservicios.Atraccion.Business.DTOs.Incluye;
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
    public class IncluyeBusinessService : IIncluyeBusinessService
    {
        private readonly IIncluyeDataService _dataService;

        public IncluyeBusinessService(IIncluyeDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<IncluyeResponse>> GetAllAsync()
        {
            var data = await _dataService.GetAllAsync();
            return data.Select(IncluyeBusinessMapper.ToResponse);
        }

        public async Task<IncluyeResponse> GetByIdAsync(int id)
        {
            var data = await _dataService.GetByIdAsync(id);
            return IncluyeBusinessMapper.ToResponse(data);
        }

        public async Task<int> CreateAsync(CreateIncluyeRequest request)
        {
            IncluyeValidator.ValidateCreate(request);
            var model = IncluyeBusinessMapper.ToCreateModel(request);
            return await _dataService.CreateAsync(model);
        }

        public async Task UpdateAsync(UpdateIncluyeRequest request)
        {
            IncluyeValidator.ValidateUpdate(request);
            var model = IncluyeBusinessMapper.ToUpdateModel(request);
            await _dataService.UpdateAsync(model);
        }

        public async Task LogicalDeleteAsync(int id)
        {
            await _dataService.SoftDeleteAsync(id);
        }
    }
}
