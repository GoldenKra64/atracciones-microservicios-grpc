using Atraccion.Microservicios.Atraccion.Business.DTOs;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using Atraccion.Microservicios.Atraccion.Business.Exceptions;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Atraccion.Microservicios.Atraccion.Business.Mappers;
using Atraccion.Microservicios.Atraccion.Business.Validators;
using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Services
{
    public class AtraccionBusinessService : IAtraccionBusinessService
    {
        private readonly IAtraccionDataService _dataService;

        public AtraccionBusinessService(IAtraccionDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task CreateAsync(CreateAtraccionRequest request)
        {
            AtraccionValidator.ValidateCreate(request);

            var model = AtraccionBusinessMapper.ToCreateModel(request);

            await _dataService.CreateAsync(model);
        }

        public async Task UpdateAsync(UpdateAtraccionRequest request)
        {
            AtraccionValidator.ValidateUpdate(request);

            var model = AtraccionBusinessMapper.ToUpdateModel(request);

            await _dataService.UpdateAsync(model);
        }

        public async Task<AtraccionDetalleDto> GetByIdAsync(string id)
        {
            var data = await _dataService.GetByIdAsync(id);

            if (data == null)
                throw new NotFoundException("Atracción", id);

            return AtraccionBusinessMapper.ToResponseDetalle(data);
        }

        public async Task<List<AtraccionResponse>> GetAllInternalAsync()
        {
            var data = await _dataService.GetAllInternalAsync();

            if (data == null)
                throw new NotFoundException("Atracción", "Atracciones no encontradas");

            return data.Select(AtraccionBusinessMapper.ToAtraccionResponse).ToList();
        }

        public async Task<PagedResponse<ListadoAtracciones>> GetPagedAsync(
            FiltroDto filtro)
        {
            var filtroModel = AtraccionBusinessMapper.ToFilterModel(filtro);
            var data = await _dataService.GetPagedAsync(filtroModel);

            return CommonBusinessMapper.ToPagedResponse(
                data,
                AtraccionBusinessMapper.ToResponse
            );
        }

        public async Task LogicalDeleteAsync(string id)
        {
            await _dataService.SoftDeleteAsync(id);
        }

        public async Task<List<AtraccionTypeResponse>> GetAtraccionType()
        {
            var data = await _dataService.GetAtraccionTypeAsync();
            return data.Select(AtraccionBusinessMapper.ToModelType).ToList();
        }

        public async Task<AtraccionResponse> GetInternalById(string id)
        {
            var data = await _dataService.GetInternalByIdAsync(id);
            return AtraccionBusinessMapper.ToAtraccionResponse(data);
        }

        public async Task<List<TicketDto>> GetTicketsByAttraction(string guid)
        {
            var data = await _dataService.GetByIdAsync(guid);
            return AtraccionBusinessMapper.MapTicketsToResponse(data);
        }

        public async Task<List<HorarioDto>> GetHorariosByAttraction(string guid)
        {
            var data = await _dataService.GetByIdAsync(guid);
            return AtraccionBusinessMapper.MapHorariosToResponse(data);
        }
    }
}