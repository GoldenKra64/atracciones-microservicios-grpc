using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Atraccion.Microservicios.Atraccion.Api.Models.Common
{
    public class ApiErrorResponse
    {
        public bool Success { get; set; } = false;
        public int Status { get; set; } = 400;
        public string Error { get; set; } = "Ocurrió un error";

        public List<string> Details { get; set; } = new();
        public string Timestamp { get; set; }
        public string? TraceId { get; set; }

        public static ApiErrorResponse Fail(string message, List<string>? errors = null, int status = 400, string? traceId = null)
        {
            return new ApiErrorResponse
            {
                Status = status,
                Error = message,
                Details = errors ?? new List<string>(),
                Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                TraceId = traceId
            };
        }
    }
}
