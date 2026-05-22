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
    public class TicketBusinessService : ITicketBusinessService
    {
        private readonly ITicketDataService _dataService;
        private readonly IHorarioDataService _horarioDataService;

        public TicketBusinessService(ITicketDataService dataService, IHorarioDataService horarioDataService)
        {
            _dataService = dataService;
            _horarioDataService = horarioDataService;
        }

        public async Task<int> CreateAsync(CreateTicketRequest request)
        {
            TicketValidator.ValidateCreate(request);

            var horario = await _horarioDataService.GetByIdAsync(request.HorarioId);
            if (horario == null) throw new ValidationException("Horario no encontrado");

            var model = TicketBusinessMapper.ToCreateModel(request);
            model.HorarioId = horario.HorarioId;
            return await _dataService.CreateAsync(model);
        }

        public async Task UpdateAsync(UpdateTicketRequest request)
        {
            TicketValidator.ValidateUpdate(request);

            var horario = await _horarioDataService.GetByIdAsync(request.HorarioId);
            if (horario == null) throw new ValidationException("Horario no encontrado");

            var model = TicketBusinessMapper.ToUpdateModel(request);
            model.HorarioId = horario.HorarioId;
            await _dataService.UpdateAsync(model);
        }

        public async Task LogicalDeleteAsync(int id)
        {
            await _dataService.SoftDeleteAsync(id);
        }

        public async Task<List<TicketRes>> GetAllAsync()
        {
            var data = await _dataService.GetAllAsync();
            return data.Select(TicketBusinessMapper.ToResponseNoHorario).ToList();
        }

        public async Task<TicketRes> GetByIdAsync(int id)
        {
            var data = await _dataService.GetByIdAsync(id);
            return TicketBusinessMapper.ToResponseNoHorario(data);
        }

        public async Task<HorarioDto> GetHorariosByTicketAsync(string guid)
        {
            var data = await _dataService.GetByGuidAsync(guid);
            var ticketData = TicketBusinessMapper.ToResponse(data);
            return HorarioBusinessMapper.ToResponse(data.Horario);
        }
    }
}
