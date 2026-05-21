using System.Text.Json.Serialization;

namespace Atraccion.Microservicios.Atraccion.Api.Models.Common
{
    public class ApiErrorResponse
    {
        [JsonPropertyName("status")]
        public int Status { get; set; } = 400;

        [JsonPropertyName("message")]
        public string Message { get; set; } = "Ocurrió un error";

        [JsonPropertyName("details")]
        public List<string> Details { get; set; } = new();

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        public static ApiErrorResponse Fail(string message, List<string>? errors = null, int status = 400, string path = "")
        {
            return new ApiErrorResponse
            {
                Status = status,
                Message = message,
                Details = errors ?? new List<string>(),
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Path = path
            };
        }
    }
}
