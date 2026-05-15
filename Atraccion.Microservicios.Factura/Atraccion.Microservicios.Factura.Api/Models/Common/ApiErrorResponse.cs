namespace Atraccion.Microservicios.Factura.Api.Models.Common
{
    public class ApiErrorResponse
    {
        public bool Success { get; set; } = false;
        public int Status { get; set; } = 404;
        public string Error { get; set; } = "Ocurrió un error";

        public List<string> Details { get; set; } = new();
        public string Timestamp { get; set; }

        public string? TraceId { get; set; }

        public static ApiErrorResponse Fail(string message, int status, List<string>? errors = null, string? traceId = null)
        {
            return new ApiErrorResponse
            {
                Status = status,
                Error = message,
                Details = errors ?? new List<string>(),
                TraceId = traceId
            };
        }
    }
}
