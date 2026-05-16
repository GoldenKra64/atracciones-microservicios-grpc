using Atraccion.Microservicios.Cliente.Api.Extensions;
using Atraccion.Microservicios.Cliente.Api.Middleware;

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
    options.ListenAnyIP(80);         // REST on port 80 (HTTP/1.1)
    options.ListenAnyIPHttp2(5000);  // gRPC on port 5000 (HTTP/2)
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