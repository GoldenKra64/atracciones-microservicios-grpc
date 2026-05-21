using Atraccion.Microservicios.Reserva.DataAccess.Common;
using Atraccion.Microservicios.Reserva.DataAccess.Entities;
using Atraccion.Microservicios.Reserva.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Reserva.DataManagement.Integrations;
using Atraccion.Microservicios.Reserva.DataManagement.Interfaces;
using Atraccion.Microservicios.Reserva.DataManagement.Mappers;
using Atraccion.Microservicios.Reserva.DataManagement.Models;
using Atraccion.Microservicios.Reserva.DataManagement.Models.Reserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Services
{
    public class ReservaDataService : IReservaDataService
    {
        private readonly IReservaQuery _query;
        private readonly IUnitOfWork _uow;

        private readonly IAtraccionIntegration _atraccionIntegration;
        private readonly IFacturaIntegration _facturaIntegration;

        public ReservaDataService(
            IReservaQuery query,
            IUnitOfWork uow,
            IAtraccionIntegration atraccionIntegration,
            IFacturaIntegration facturaIntegration)
        {
            _query = query;
            _uow = uow;
            _atraccionIntegration = atraccionIntegration;
            _facturaIntegration = facturaIntegration;
        }

        public async Task<DataPagedResult<ReservaModel>> GetByClienteAsync(int clienteId, int page, int size)
        {
            var result = await _query.GetByClienteAsync(clienteId, page, size);

            return new DataPagedResult<ReservaModel>
            {
                Items = result.Items.Select(ReservaMapper.ToModel),
                TotalRecords = result.TotalRecords,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<ReservaModel?> GetDetalleAsync(int reservaId)
        {
            var entity = await _query.GetDetalleAsync(reservaId);
            return entity == null ? null : ReservaMapper.ToModel(entity);
        }

        public async Task<ReservaModel> CreateAsync(ReservaCreateModel model, bool isPublic = false)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            await _uow.BeginTransactionAsync();

            try
            {
                var horario = await _atraccionIntegration.GetHorarioByGuidAsync(model.HorarioGuid);
                if (horario == null) throw new ArgumentNullException(nameof(horario));

                var entity = ReservaMapper.ToEntity(model, horario);
                foreach (var linea in model.Lineas)
                {
                    var ticketInfo = await _atraccionIntegration.GetTicketInfoByGuidAsync(linea.TicketId);
                    if (ticketInfo == null) throw new Exception($"Ticket {linea.TicketId} no encontrado");

                    var det = new DetalleReserva
                    {
                        DetRevGuid = Guid.NewGuid().ToString(),
                        TicId = ticketInfo.TicId,
                        TicGuid = linea.TicketId,
                        TicTipoParticipante = ticketInfo.TicTipoParticipante,
                        TicCantidad = linea.Cantidad,
                        TicPrecioUnitario = ticketInfo.TicPrecio,
                        TicSubtotal = ticketInfo.TicPrecio * linea.Cantidad,
                        TicTitulo = ticketInfo.TicTitulo,
                        Reserva = entity,
                        RevId = entity.RevId
                    };

                    entity.Detalles.Add(det);
                }

                // Totals
                entity.RevSubtotal = entity.Detalles.Sum(x => x.TicSubtotal);
                entity.RevValorIva = (entity.RevSubtotal * 0.15);
                entity.RevTotal = (double)(entity.RevSubtotal + entity.RevValorIva);

                if (!isPublic)
                {
                    entity.RevEstado = "APR";

                    foreach (var det in entity.Detalles)
                    {
                        await _atraccionIntegration.ConsumeCapacityAsync(horario.HorId, det.TicCantidad);
                    }
                }

                var id = await _uow.ReservaRepository.CreateWithDetallesAsync(entity);

                if (!isPublic)
                {
                    await _facturaIntegration.GenerateInvoiceAsync(new GenerateInvoiceDto
                    {
                        RevId = id,
                        CliId = entity.CliId ?? 0,
                        Canal = entity.RevCanal,
                        Total = entity.RevTotal,
                        NombreReceptor = null,
                        CorreoReceptor = null
                    });
                }

                await _uow.CommitAsync();

                return ReservaMapper.ToModel(entity);
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

        public async Task SoftDeleteAsync(string reservaId)
        {
            await _uow.ReservaRepository.SoftDeleteAsync(reservaId);
        }

        public async Task<Atraccion.Microservicios.Reserva.DataManagement.Protos.GenerateInvoiceResponse> ApproveAsync(string id, string? nombreReceptor, string? correoReceptor)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var entity = await _query.GetByIdAsync(id);
                if (entity == null) throw new Exception("Reserva no encontrada");

                if (entity.RevEstado == "APR")
                    throw new Exception("La reserva ya se encuentra aprobada.");

                // Asumimos que podemos obtener el HorId desde algún lado, en el diseño real
                // deberías guardar el HorId directamente en la Reserva para no perderlo.
                // Por ahora, asumiremos que se guardó en `entity` o hacemos una consulta inversa,
                // pero la lógica dicta que Reserva DEBE tener el HorId.

                // MOCK: Para que compile, suponemos que todas las líneas usan el mismo horario.
                // En tu diseño, agregaste HorFecha y HorHoraInicio, pero no HorId. 
                // Necesitarás agregar HorId a Reserva.cs.

                entity.RevEstado = "APR";

                // 🔄 Llamada "gRPC" al microservicio de Atraccion (Consumir capacidad)
                foreach (var det in entity.Detalles)
                {
                    // Nota: Idealmente deberías tener el HorId en DetalleReserva o Reserva.
                    // Para evitar que falle, pasamos un ID dummy u obtenemos el ticket de nuevo
                    var ticketInfo = await _atraccionIntegration.GetTicketInfoAsync(det.TicId);
                    await _atraccionIntegration.ConsumeCapacityAsync(ticketInfo.HorId, det.TicCantidad);
                }

                await _uow.ReservaRepository.ApproveAsync(id);

                // 🔄 Llamada "gRPC" al microservicio de Factura
                var response = await _facturaIntegration.GenerateInvoiceAsync(new GenerateInvoiceDto
                {
                    RevId = entity.RevId,
                    CliId = entity.CliId ?? 0,
                    Canal = entity.RevCanal,
                    Total = entity.RevTotal,
                    NombreReceptor = nombreReceptor,
                    CorreoReceptor = correoReceptor
                });

                await _uow.CommitAsync();
                return response;
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

        public async Task<ReservaModel?> GetByIdAsync(string id)
        {
            var entity = await _query.GetByIdAsync(id);
            return entity == null ? null : ReservaMapper.ToModel(entity);
        }

        public async Task<List<ReservaModel?>> GetAllAsync()
        {
            var entity = await _query.GetAllAsync();
            return entity == null ? null : entity.Select(ReservaMapper.ToModel).ToList();
        }

        public async Task<ReservaModel?> UpdateAsync(UpdateReservaModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            await _uow.BeginTransactionAsync();

            try
            {
                var entity = await _query.GetByIdAsync(model.Id)
                    ?? throw new Exception("Reserva no encontrada");

                var horario = await _atraccionIntegration.GetHorarioByGuidAsync(model.HorarioGuid);
                if (horario == null) throw new ArgumentNullException(nameof(horario));

                entity.CliId = model.ClienteId;
                entity.RevCanal = model.Canal;

                entity.HorFecha = horario.HorFecha;
                entity.HorHoraInicio = horario.HorHoraInicio;
                entity.HorHoraFin = horario.HorHoraFin;

                var detallesExistentes = entity.Detalles.ToDictionary(d => d.TicId);
                var detallesNuevos = new List<DetalleReserva>();

                foreach (var linea in model.Lineas)
                {
                    var ticket = await _atraccionIntegration.GetTicketInfoByGuidAsync(linea.TicketId);
                    if (ticket == null) throw new Exception($"Ticket {linea.TicketId} no encontrado");

                    if (detallesExistentes.TryGetValue(ticket.TicId, out var detExistente))
                    {
                        detExistente.TicCantidad = linea.Cantidad;
                        detExistente.TicPrecioUnitario = ticket.TicPrecio;
                        detExistente.TicSubtotal = ticket.TicPrecio * linea.Cantidad;
                        detallesExistentes.Remove(ticket.TicId);
                    }
                    else
                    {
                        var det = new DetalleReserva
                        {
                            DetRevGuid = Guid.NewGuid().ToString(),
                            TicId = ticket.TicId,
                            TicGuid = linea.TicketId,
                            TicTipoParticipante = ticket.TicTipoParticipante,
                            TicCantidad = linea.Cantidad,
                            TicPrecioUnitario = ticket.TicPrecio,
                            TicSubtotal = ticket.TicPrecio * linea.Cantidad,
                            TicTitulo = ticket.TicTitulo,
                            Reserva = entity,
                            RevId = entity.RevId
                        };
                        detallesNuevos.Add(det);
                    }
                }

                foreach (var d in detallesExistentes.Values)
                {
                    await _uow.ReservaRepository.DeleteDetalleAsync(d);
                    entity.Detalles.Remove(d);
                }

                foreach (var n in detallesNuevos)
                {
                    entity.Detalles.Add(n);
                }

                entity.RevSubtotal = entity.Detalles.Sum(x => x.TicSubtotal);
                entity.RevValorIva = (entity.RevSubtotal * 0.15);
                entity.RevTotal = entity.RevSubtotal + entity.RevValorIva;

                var updatedEntity = await _uow.ReservaRepository.UpdateAsync(entity);

                await _uow.CommitAsync();

                return ReservaMapper.ToModel(updatedEntity);
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

        public async Task CancelAsync(string id)
        {
            await _uow.ReservaRepository.CancelAsync(id);
        }

        public async Task<DataPagedResult<ReservaModel>> GetAllBookingAsync(int page, int size)
        {
            var result = await _query.GetAllBookingAsync(page, size);

            return new DataPagedResult<ReservaModel>
            {
                Items = result.Items.Select(ReservaMapper.ToModel),
                TotalRecords = result.TotalRecords,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }
    }
}
