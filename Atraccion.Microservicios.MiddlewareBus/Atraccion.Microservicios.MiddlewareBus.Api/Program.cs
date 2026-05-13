var builder = WebApplication.CreateBuilder(args);

// Configurar YARP (Reverse Proxy) usando la configuración en appsettings.json
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapear el Reverse Proxy (YARP)
app.MapReverseProxy();

app.MapControllers();

app.Run();
