using Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
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
    public class HorarioBusinessService : IHorarioBusinessService
    {
        private readonly IHorarioDataService _dataService;

        public HorarioBusinessService(IHorarioDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<int> CreateAsync(CreateHorarioRequest request)
        {
            HorarioValidator.ValidateCreate(request);

            var model = HorarioBusinessMapper.ToCreateModel(request);
            return await _dataService.CreateAsync(model);
        }

        public async Task<IEnumerable<HorarioDto>> GetAllAsync()
        {
            var data = await _dataService.GetAllAsync();
            return data.Select(HorarioBusinessMapper.ToResponse);
        }

        public async Task<HorarioDto> GetByIdAsync(string id)
        {
            var data = await _dataService.GetByIdAsync(id);
            return HorarioBusinessMapper.ToResponse(data);
        }

        public async Task LogicalDeleteAsync(int id)
        {
            await _dataService.SoftDeleteAsync(id);
        }

        public async Task UpdateAsync(UpdateHorarioRequest request)
        {
            var horario = await _dataService.GetByIdAsync(request.Guid);
            request.Id = horario.HorarioId;

            HorarioValidator.ValidateUpdate(request);

            var model = HorarioBusinessMapper.ToUpdateModel(request);
            await _dataService.UpdateAsync(model);
        }
    }
}
