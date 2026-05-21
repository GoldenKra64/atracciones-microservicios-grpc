using Atraccion.Microservicios.Reserva.DataManagement.Interfaces;
using Atraccion.Microservicios.Reserva.DataManagement.Protos;
using System;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Integrations
{
    public class AtraccionIntegrationGrpc : IAtraccionIntegration
    {
        private readonly AtraccionService.AtraccionServiceClient _client;

        public AtraccionIntegrationGrpc(AtraccionService.AtraccionServiceClient client)
        {
            _client = client;
        }

        public async Task ConsumeCapacityAsync(int horId, int cantidad)
        {
            await _client.ConsumeCapacityAsync(new ConsumeCapacityRequest
            {
                HorId = horId,
                Cantidad = cantidad
            });
        }

        public async Task<HorarioIntegrationDto> GetHorarioByGuidAsync(string guid)
        {
            var response = await _client.GetHorarioByGuidAsync(new HorarioRequest
            {
                Guid = guid
            });

            return new HorarioIntegrationDto
            {
                HorId = response.HorId,
                HorGuid = response.HorGuid,
                CuposDisponibles = response.CuposDisponibles,
                HorFecha = response.HorFecha,
                HorHoraInicio = response.HorHoraInicio,
                HorHoraFin = response.HorHoraFin
            };
        }

        public async Task<TicketIntegrationDto> GetTicketInfoAsync(int ticId)
        {
            var response = await _client.GetTicketInfoAsync(new TicketRequest
            {
                TicId = ticId
            });

            return new TicketIntegrationDto
            {
                TicId = response.TicId,
                TicTitulo = response.TicTitulo,
                TicPrecio = response.TicPrecio,
                TicTipoParticipante = response.TicTipoParticipante,
                HorId = response.HorId,
                AtNombre = response.AtNombre
            };
        }

        public async Task<TicketIntegrationDto> GetTicketInfoByGuidAsync(string ticGuid)
        {
            var response = await _client.GetTicketInfoAsync(new TicketRequest
            {
                TicGuid = ticGuid
            });

            return new TicketIntegrationDto
            {
                TicId = response.TicId,
                TicTitulo = response.TicTitulo,
                TicPrecio = response.TicPrecio,
                TicTipoParticipante = response.TicTipoParticipante,
                HorId = response.HorId,
                AtNombre = response.AtNombre
            };
        }
    }
}
