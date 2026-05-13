namespace Atraccion.Microservicios.Atraccion.Api.Models.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int? Status { get; set; } = 200;
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "", int? status = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Status = status,
                Data = data,
                Message = message
            };
        }
    }
}