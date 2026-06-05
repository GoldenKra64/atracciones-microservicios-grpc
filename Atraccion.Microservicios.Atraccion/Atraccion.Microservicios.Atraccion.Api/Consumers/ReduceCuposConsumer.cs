using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using Atracciones.Microservicios.Messages;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Api.Consumers
{
    public class ReduceCuposConsumer : IConsumer<ReduceCuposCommand>
    {
        private readonly IUnitOfWork _uow;

        public ReduceCuposConsumer(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Consume(ConsumeContext<ReduceCuposCommand> context)
        {
            var message = context.Message;

            var horario = await _uow.HorarioRepository.GetByIdAsync(message.HorarioId);
            if (horario == null)
            {
                throw new Exception($"Horario con Id {message.HorarioId} no encontrado.");
            }

            if (horario.HorCuposDisponibles < message.Cantidad)
            {
                throw new Exception($"No hay suficientes cupos. Disponibles: {horario.HorCuposDisponibles}. Solicitados: {message.Cantidad}.");
            }

            horario.HorCuposDisponibles -= message.Cantidad;

            await _uow.HorarioRepository.UpdateAsync(horario);
            await _uow.SaveChangesAsync();
        }
    }
}
