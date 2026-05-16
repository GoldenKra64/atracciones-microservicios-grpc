using Atraccion.Microservicios.Atraccion.Api.Extensions;
using Atraccion.Microservicios.Atraccion.Api.Middleware;
using Microsoft.AspNetCore.Mvc;
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
app.MapGrpcService<Atraccion.Microservicios.Atraccion.Api.Grpc.AtraccionGrpcService>();

app.Run();