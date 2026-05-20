using System.Text.Json.Serialization;

namespace Atraccion.Microservicios.Reserva.Api.Models.Common
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        
        [JsonPropertyName("status")]
        public int? Status { get; set; } = 200;
        
        [JsonPropertyName("data")]
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "", int? status = 200)
        {
            return new ApiResponse<T>
            {
                Status = status,
                Data = data,
                Message = message
            };
        }
    }
}
