using Atraccion.Microservicios.Reserva.Api.Extensions;
using Atraccion.Microservicios.Reserva.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// EXTENSIONS (CONFIGURACIÓN)
// ===============================
builder.Services.AddApiVersioningExtension();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsExtension();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerExtension();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ===============================
// BUILD
// ===============================
var app = builder.Build();

// ===============================
// MIDDLEWARE
// ===============================

// Manejo global de errores (primero)
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Swagger
app.UseSwaggerExtension();
app.UseSwagger();
app.UseSwaggerUI();

// CORS
app.UseCorsExtension();

// Seguridad
app.UseAuthentication();
app.UseAuthorization();

// ===============================
// ENDPOINTS
// ===============================
app.MapControllers();

app.Run();