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
        private readonly IAtraccionDataService _atraccionDataService;

        public HorarioBusinessService(IHorarioDataService dataService, IAtraccionDataService atraccionDataService)
        {
            _dataService = dataService;
            _atraccionDataService = atraccionDataService;
        }

        public async Task<int> CreateAsync(CreateHorarioRequest request)
        {
            HorarioValidator.ValidateCreate(request);

            var atraccion = await _atraccionDataService.GetByIdAsync(request.AtraccionId);
            if (atraccion == null) throw new ValidationException("Atraccion no encontrada");

            var model = HorarioBusinessMapper.ToCreateModel(request);
            model.AtraccionId = atraccion.Id;
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

            var atraccion = await _atraccionDataService.GetByIdAsync(request.AtraccionId);
            if (atraccion == null) throw new ValidationException("Atraccion no encontrada");

            var model = HorarioBusinessMapper.ToUpdateModel(request);
            model.AtraccionId = atraccion.Id;
            await _dataService.UpdateAsync(model);
        }
    }
}
