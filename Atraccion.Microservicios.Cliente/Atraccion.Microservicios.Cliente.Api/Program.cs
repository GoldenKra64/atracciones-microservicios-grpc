using Atraccion.Microservicios.Cliente.Api.Extensions;
using Atraccion.Microservicios.Cliente.Api.Middleware;
using Microsoft.AspNetCore.Server.Kestrel.Core;

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
builder.Services.AddGrpc(); // Agregado para gRPC Server

// ===============================
// KESTREL (PROTOCOL CONFIGURATION)
// ===============================
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);

    options.ListenAnyIP(5000, o =>
    {
        o.Protocols = HttpProtocols.Http2;
    });
});

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
app.MapGrpcService<Atraccion.Microservicios.Cliente.Api.Grpc.ClienteGrpcService>();

app.Run();