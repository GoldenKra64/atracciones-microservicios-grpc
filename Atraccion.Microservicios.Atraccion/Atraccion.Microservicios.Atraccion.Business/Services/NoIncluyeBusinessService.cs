using Atraccion.Microservicios.Atraccion.Business.DTOs.NoIncluye;
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
    public class NoIncluyeBusinessService : INoIncluyeBusinessService
    {
        private readonly INoIncluyeDataService _dataService;

        public NoIncluyeBusinessService(INoIncluyeDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<NoIncluyeResponse>> GetAllAsync()
        {
            var data = await _dataService.GetAllAsync();
            return data.Select(NoIncluyeBusinessMapper.ToResponse);
        }

        public async Task<int> CreateAsync(CreateNoIncluyeRequest request)
        {
            NoIncluyeValidator.ValidateCreate(request);
            var model = NoIncluyeBusinessMapper.ToCreateModel(request);
            return await _dataService.CreateAsync(model);
        }

        public async Task UpdateAsync(UpdateNoIncluyeRequest request)
        {
            NoIncluyeValidator.ValidateUpdate(request);
            var model = NoIncluyeBusinessMapper.ToUpdateModel(request);
            await _dataService.UpdateAsync(model);
        }

        public async Task LogicalDeleteAsync(int id)
        {
            await _dataService.SoftDeleteAsync(id);
        }

        public async Task<NoIncluyeResponse> GetByIdAsync(int id)
        {
            var data = await _dataService.GetByIdAsync(id);
            return NoIncluyeBusinessMapper.ToResponse(data);
        }
    }
}
