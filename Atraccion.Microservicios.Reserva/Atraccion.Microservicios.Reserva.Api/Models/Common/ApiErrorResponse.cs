namespace Atraccion.Microservicios.Reserva.Api.Models.Common
{
    public class ApiErrorResponse
    {
        public int Status { get; set; }
        public string Message { get; set; } = "Ocurrió un error";

        public List<string> Errors { get; set; } = new();

        public string? TraceId { get; set; }

        public static ApiErrorResponse Fail(int status, string message, List<string>? errors = null, string? traceId = null)
        {
            return new ApiErrorResponse
            {
                Message = message,
                Status = status,
                Errors = errors ?? new List<string>(),
                TraceId = traceId
            };
        }
    }
}
