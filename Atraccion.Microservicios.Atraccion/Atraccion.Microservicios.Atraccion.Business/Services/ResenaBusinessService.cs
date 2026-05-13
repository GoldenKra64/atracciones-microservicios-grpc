using Atraccion.Microservicios.Atraccion.Business.DTOs.Resena;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Atraccion.Microservicios.Atraccion.Business.Mappers;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Services
{
    public class ResenaBusinessService : IResenaBusinessService
    {
        private readonly IResenaDataService _dataService;

        public ResenaBusinessService(IResenaDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<ResenaResponse>> GetByAtraccionAsync(string atraccionId)
        {
            var data = await _dataService.GetByAtraccionAsync(atraccionId);
            return data.Select(ResenaBusinessMapper.ToResponse);
        }

        public async Task<int> CreateAsync(CreateResenaRequest request)
        {
            var model = ResenaBusinessMapper.ToCreateModel(request);
            return await _dataService.CreateAsync(model);
        }

        public async Task UpdateAsync(UpdateResenaRequest request)
        {
            var model = ResenaBusinessMapper.ToUpdateModel(request);
            await _dataService.UpdateAsync(model);
        }

        public async Task LogicalDeleteAsync(int id)
        {
            await _dataService.SoftDeleteAsync(id);
        }
    }
}
