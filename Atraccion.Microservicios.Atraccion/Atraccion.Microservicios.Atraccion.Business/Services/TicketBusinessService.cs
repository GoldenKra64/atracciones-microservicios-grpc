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

        public TicketBusinessService(ITicketDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<int> CreateAsync(CreateTicketRequest request)
        {
            TicketValidator.ValidateCreate(request);
            var model = TicketBusinessMapper.ToCreateModel(request);
            return await _dataService.CreateAsync(model);
        }

        public async Task UpdateAsync(UpdateTicketRequest request)
        {
            TicketValidator.ValidateUpdate(request);
            var model = TicketBusinessMapper.ToUpdateModel(request);
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
    }
}
