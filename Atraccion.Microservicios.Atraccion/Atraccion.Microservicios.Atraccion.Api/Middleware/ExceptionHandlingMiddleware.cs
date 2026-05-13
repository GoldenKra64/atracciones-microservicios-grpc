using Atraccion.Microservicios.Atraccion.Api.Models.Common;
using Atraccion.Microservicios.Atraccion.Business.Exceptions;
using System.Net;
using System.Text.Json;

namespace Atraccion.Microservicios.Atraccion.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var traceId = context.TraceIdentifier;
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Error interno del servidor";
            var errors = new List<string> { };

            if (exception is ValidationException valExcept)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = valExcept.Message;
                var validationMessages = valExcept.Errors
                    .SelectMany(kvp => kvp.Value.Select(v => string.IsNullOrWhiteSpace(kvp.Key) ? v : $"{kvp.Key}: {v}"));
                errors.AddRange(validationMessages);
            }
            else if (exception is UnauthorizedBusinessException unauthEx)
            {
                statusCode = StatusCodes.Status401Unauthorized;
                message = unauthEx.Message;
                errors.Add(unauthEx.Message);
            }
            else if (exception is BusinessException businessEx)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = businessEx.Message;
                errors.Add(businessEx.Message);
                if (!string.IsNullOrEmpty(businessEx.ErrorCode))
                {
                    errors.Add($"ErrorCode: {businessEx.ErrorCode}");
                }
            }
            else if (exception is NotFoundException notFoundEx)
            {
                statusCode = StatusCodes.Status404NotFound;
                message = notFoundEx.Message;
                errors.Add(notFoundEx.Message);
            }
            else
            {
                errors.Add(exception.InnerException?.ToString() ?? exception.ToString());
            }

            var response = ApiErrorResponse.Fail(
                message,
                errors,
                traceId
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
