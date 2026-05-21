using System.Text.Json.Serialization;

namespace Atraccion.Microservicios.Reserva.Api.Models.Common
{
    public class ApiErrorResponse
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = "Ocurrió un error";

        [JsonPropertyName("details")]
        public List<string> Details { get; set; } = new();

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        public static ApiErrorResponse Fail(int status, string message, List<string>? errors = null, string path = "")
        {
            return new ApiErrorResponse
            {
                Message = message,
                Status = status,
                Details = errors ?? new List<string>(),
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Path = path
            };
        }
    }
}
