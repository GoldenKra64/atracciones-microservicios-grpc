using Atraccion.Microservicios.Reserva.Business.DTOs;
using Atraccion.Microservicios.Reserva.Business.DTOs.Reserva;
using Atraccion.Microservicios.Reserva.Business.Exceptions;
using Atraccion.Microservicios.Reserva.Business.Interfaces;
using Atraccion.Microservicios.Reserva.Business.Mappers;
using Atraccion.Microservicios.Reserva.Business.Validators;
using Atraccion.Microservicios.Reserva.DataManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.Business.Services
{
    public class ReservaBusinessService : IReservaBusinessService
    {
        private readonly IReservaDataService _dataService;
        private readonly IAtraccionIntegration _atraccionIntegration;

        public ReservaBusinessService(IReservaDataService dataService, IAtraccionIntegration atraccionIntegration)
        {
            _dataService = dataService;
            _atraccionIntegration = atraccionIntegration;
        }

        public async Task<ReservaResponse> CreateAsync(CreateReservaRequest request)
        {
            ReservaValidator.ValidateCreate(request);

            if (request.Lineas == null || !request.Lineas.Any())
                throw new ValidationException("Debe incluir al menos un detalle en la reserva.");

            foreach (var linea in request.Lineas)
            {
                if (linea.tck_guid == null)
                    throw new ValidationException($"Ticket {linea.tck_guid} not found");
            }

            var horario = await _atraccionIntegration.GetHorarioByGuidAsync(request.hor_guid);
            if (horario == null)
                throw new ValidationException($"Horario {request.hor_guid} not found");
            
            int cantidad = 0;
            foreach (var linea in request.Lineas)
            {
                cantidad += linea.cantidad;
            }

            if (cantidad > horario.CuposDisponibles)
                throw new ValidationException($"No hay suficientes cupos para ese horario");

            var model = ReservaBusinessMapper.ToCreateModel(request);

            var created = await _dataService.CreateAsync(model);

            return ReservaBusinessMapper.ToResponse(created);
        }

        public async Task<ReservaResponse> CreatePublicAsync(CreateReservaRequest request)
        {

            ReservaValidator.ValidateCreate(request);

            if (request.Lineas == null || !request.Lineas.Any())
                throw new ValidationException("Debe incluir al menos un detalle en la reserva.");

            foreach (var linea in request.Lineas)
            {
                if (linea.tck_guid == null)
                    throw new ValidationException($"Ticket {linea.tck_guid} not found");
            }

            var horario = await _atraccionIntegration.GetHorarioByGuidAsync(request.hor_guid);
            if (horario == null)
                throw new ValidationException($"Horario {request.hor_guid} not found");


            int cantidad = 0;
            foreach (var linea in request.Lineas)
            {
                cantidad += linea.cantidad;
            }

            if (cantidad > horario.CuposDisponibles)
                throw new ValidationException($"No hay suficientes cupos para ese horario");


            var model = ReservaBusinessMapper.ToCreateModel(request);

            var created = await _dataService.CreateAsync(model, true);

            return ReservaBusinessMapper.ToResponse(created);
        }

        public async Task<ReservaResponse> GetByIdAsync(string id)
        {
            var data = await _dataService.GetByIdAsync(id);

            if (data == null)
                throw new NotFoundException("Reserva", id);

            return ReservaBusinessMapper.ToResponse(data);
        }

        public async Task<PagedResponse<ReservaResponse>> GetByClienteAsync(
            int clienteId,
            int page,
            int size)
        {
            var data = await _dataService.GetByClienteAsync(clienteId, page, size);

            return CommonBusinessMapper.ToPagedResponse(
                data,
                ReservaBusinessMapper.ToResponse
            );
        }

        public async Task LogicalDeleteAsync(string id)
        {
            await _dataService.SoftDeleteAsync(id);
        }

        public async Task<List<ReservaResponse>> GetAllAsync()
        {
            var data = await _dataService.GetAllAsync();
            return data.Select(ReservaBusinessMapper.ToResponse).ToList();
        }

        public async Task<ReservaResponse> UpdateAsync(UpdateReservaRequest request)
        {
            ReservaValidator.ValidateUpdate(request);

            var model = ReservaBusinessMapper.ToUpdateModel(request);
            var data = await _dataService.UpdateAsync(model);

            return ReservaBusinessMapper.ToResponse(data);
        }

        public async Task CancelarAsync(string id, CancelarReservaRequest request)
        {
            await _dataService.CancelAsync(id);
        }

        public async Task<Atraccion.Microservicios.Reserva.Business.DTOs.Factura.FacturaResponse> ApproveAsync(string id, ConfirmarPagoRequest request)
        {
            var invoice = await _dataService.ApproveAsync(id, request?.nombre_receptor, request?.correo_receptor);
            var reserva = await _dataService.GetByIdAsync(id);

            return new Atraccion.Microservicios.Reserva.Business.DTOs.Factura.FacturaResponse
            {
                FacGuid = invoice.FacGuid,
                FacNumero = invoice.FacNumero,
                RevCodigo = reserva?.rev_codigo ?? string.Empty,
                Total = invoice.Total,
                Moneda = "USD",
                FechaEmision = invoice.FechaEmision,
                Estado = invoice.Estado,
                NombreReceptor = invoice.NombreReceptor,
                CorreoReceptor = invoice.CorreoReceptor
            };
        }
    }
}
