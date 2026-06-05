using System;

namespace Atracciones.Microservicios.Messages
{
    public interface ReduceCuposCommand
    {
        int HorarioId { get; }
        int Cantidad { get; }
    }

    public interface GenerateInvoiceCommand
    {
        int RevId { get; }
        int CliId { get; }
        string Canal { get; }
        double Total { get; }
        string? NombreReceptor { get; }
        string? CorreoReceptor { get; }
    }

    public interface GenerateInvoiceResult
    {
        string FacGuid { get; }
        string FacNumero { get; }
        string RevCodigo { get; }
        double Total { get; }
        DateTime FechaEmision { get; }
        string? NombreReceptor { get; }
        string? CorreoReceptor { get; }
    }
}
