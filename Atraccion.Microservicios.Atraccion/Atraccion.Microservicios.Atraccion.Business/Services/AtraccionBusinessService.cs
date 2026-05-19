using Atraccion.Microservicios.Atraccion.Business.DTOs;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Filters;
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

        public async Task<AtraccionesPagedResponseDto> GetPagedV2Async(
            FiltroDto filtro)
        {
            var filtroModel = AtraccionBusinessMapper.ToFilterModel(filtro);
            var data = await _dataService.GetPagedAsync(filtroModel);
            var unFilteredProductCount = await _dataService.GetActiveCountAsync();

            return new AtraccionesPagedResponseDto
            {
                Items = data.Items.Select(AtraccionBusinessMapper.ToResponse).ToList(),
                TotalRecords = data.TotalRecords,
                PageNumber = data.PageNumber,
                PageSize = data.PageSize,
                TotalPages = (int)Math.Ceiling((double)data.TotalRecords / data.PageSize),
                UnfilteredProductCount = unFilteredProductCount
            };
        }

        public async Task LogicalDeleteAsync(string id)
        {
            await _dataService.SoftDeleteAsync(id);
        }

        public async Task<FiltrosDisponibles> GetFiltrosAsync()
        {
            var atracciones = await _dataService.GetAllInternalAsync();

            var result = new FiltrosDisponibles();

            // Destination Filters
            result.destinationFilters = atracciones
                .Where(a => a?.Destino != null && !string.IsNullOrEmpty(a.Destino.Nombre))
                .GroupBy(a => a!.Destino.Nombre)
                .Select(g => new OpcionFiltro
                {
                    nombre = g.Key,
                    tagname = g.Key.ToLower().Replace(" ", "_"),
                    productCount = g.Count()
                })
                .OrderByDescending(o => o.productCount)
                .ToList();

            // Type Filters
            result.typeFilters = atracciones
                .Where(a => a?.Categorias != null)
                .SelectMany(a => a!.Categorias)
                .Where(c => !string.IsNullOrEmpty(c.Nombre))
                .GroupBy(c => c.Nombre)
                .Select(g => new OpcionFiltro
                {
                    nombre = g.Key,
                    tagname = g.Key.ToLower().Replace(" ", "_"),
                    productCount = g.Count()
                })
                .OrderByDescending(o => o.productCount)
                .ToList();

            // Label Filters
            result.labelFilters = atracciones
                .Where(a => a?.TagAtracciones != null)
                .SelectMany(a => a!.TagAtracciones)
                .Where(t => !string.IsNullOrEmpty(t.Nombre))
                .GroupBy(t => t.Nombre)
                .Select(g => new OpcionFiltro
                {
                    nombre = g.Key,
                    tagname = g.Key.ToLower().Replace(" ", "_"),
                    productCount = g.Count()
                })
                .OrderByDescending(o => o.productCount)
                .ToList();

            // Supported Language Filters
            result.supportedLanguageFilters = atracciones
                .Where(a => a?.Idiomas != null)
                .SelectMany(a => a!.Idiomas)
                .Where(i => !string.IsNullOrEmpty(i.Nombre))
                .GroupBy(i => i.Nombre)
                .Select(g => new OpcionFiltro
                {
                    nombre = g.Key,
                    tagname = g.Key.ToLower().Replace(" ", "_"),
                    productCount = g.Count()
                })
                .OrderByDescending(o => o.productCount)
                .ToList();

            return result;
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

        public async Task<List<TicketDto>> GetTicketsByHorario(string guid, string horarioId)
        {
            var data = await _dataService.GetByIdAsync(guid);
            if (data == null) throw new NotFoundException("Atracción", guid);
            return AtraccionBusinessMapper.MapTicketsByHorarioToResponse(data, horarioId);
        }
    }
}