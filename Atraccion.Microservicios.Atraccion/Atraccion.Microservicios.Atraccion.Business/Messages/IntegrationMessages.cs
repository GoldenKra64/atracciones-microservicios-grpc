using System;

namespace Atracciones.Microservicios.Messages
{
    public interface ReduceCuposCommand
    {
        int HorarioId { get; }
        int Cantidad { get; }
    }
}
