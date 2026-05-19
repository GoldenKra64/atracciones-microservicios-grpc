using Atraccion.Microservicios.Atraccion.Api.Protos;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Api.Grpc
{
    public class AtraccionGrpcService : AtraccionService.AtraccionServiceBase
    {
        private readonly ITicketDataService _ticketService;
        private readonly IHorarioDataService _horarioService;
        private readonly IUnitOfWork _uow;

        public AtraccionGrpcService(ITicketDataService ticketService, IHorarioDataService horarioService, IUnitOfWork uow)
        {
            _ticketService = ticketService;
            _horarioService = horarioService;
            _uow = uow;
        }

        public override async Task<TicketResponse> GetTicketInfo(TicketRequest request, ServerCallContext context)
        {
            try
            {
                Atraccion.DataManagement.Models.Ticket.TicketModel ticket = null;
                
                if (!string.IsNullOrEmpty(request.TicGuid))
                {
                    ticket = await _ticketService.GetByGuidAsync(request.TicGuid);
                    if (ticket == null)
                        throw new RpcException(new Status(StatusCode.NotFound, $"Ticket con Guid {request.TicGuid} no encontrado."));
                }
                else
                {
                    ticket = await _ticketService.GetByIdAsync(request.TicId);
                    if (ticket == null)
                        throw new RpcException(new Status(StatusCode.NotFound, $"Ticket con Id {request.TicId} no encontrado."));
                }

                return new TicketResponse
                {
                    TicId = ticket.Id,
                    TicTitulo = ticket.Nombre,
                    TicPrecio = (double) ticket.Precio,
                    TicTipoParticipante = ticket.Tipo,
                    HorId = ticket.HorarioId
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<HorarioResponse> GetHorarioByGuid(HorarioRequest request, ServerCallContext context)
        {
            try
            {
                var horario = await _horarioService.GetByIdAsync(request.Guid);
                if (horario == null)
                    throw new RpcException(new Status(StatusCode.NotFound, $"Horario con Guid {request.Guid} no encontrado."));

                return new HorarioResponse
                {
                    HorId = horario.HorarioId,
                    HorGuid = horario.HorarioGuid,
                    CuposDisponibles = horario.Cupos,
                    HorFecha = horario.Fecha,
                    HorHoraInicio = horario.HoraInicio,
                    HorHoraFin = horario.HoraFin ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<ConsumeCapacityResponse> ConsumeCapacity(ConsumeCapacityRequest request, ServerCallContext context)
        {
            try
            {
                var horario = await _uow.HorarioRepository.GetByIdAsync(request.HorId);
                if (horario == null)
                    throw new RpcException(new Status(StatusCode.NotFound, $"Horario con Id {request.HorId} no encontrado."));

                if (horario.HorCuposDisponibles < request.Cantidad)
                    throw new RpcException(new Status(StatusCode.FailedPrecondition, $"No hay suficientes cupos. Disponibles: {horario.HorCuposDisponibles}. Solicitados: {request.Cantidad}."));

                horario.HorCuposDisponibles -= request.Cantidad;

                await _uow.HorarioRepository.UpdateAsync(horario);
                await _uow.SaveChangesAsync();

                return new ConsumeCapacityResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
}
