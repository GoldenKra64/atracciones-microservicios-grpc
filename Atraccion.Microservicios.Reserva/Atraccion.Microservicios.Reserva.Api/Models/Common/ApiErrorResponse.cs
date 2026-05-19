namespace Atraccion.Microservicios.Reserva.Api.Models.Common
{
    public class ApiErrorResponse
    {
        public int Status { get; set; }
        public string Error { get; set; } = "Ocurrió un error";

        public List<string> Details { get; set; } = new();

        public string? TraceId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiErrorResponse Fail(int status, string message, List<string>? errors = null, string? traceId = null)
        {
            return new ApiErrorResponse
            {
                Error = message,
                Status = status,
                Details = errors ?? new List<string>(),
                TraceId = traceId,
                Timestamp = DateTime.Now
            };
        }
    }
}
