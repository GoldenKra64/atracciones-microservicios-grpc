namespace Atraccion.Microservicios.Cliente.Api.Models.Common
{
    public class ApiErrorResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "Ocurrió un error";

        public List<string> Errors { get; set; } = new();

        public string? TraceId { get; set; }

        public static ApiErrorResponse Fail(string message, List<string>? errors = null, string? traceId = null)
        {
            return new ApiErrorResponse
            {
                Message = message,
                Errors = errors ?? new List<string>(),
                TraceId = traceId
            };
        }
    }
}
