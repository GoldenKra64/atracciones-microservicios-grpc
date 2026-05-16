var builder = WebApplication.CreateBuilder(args);

// Configurar YARP (Reverse Proxy) usando la configuración en appsettings.json
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

// Mapear al Reverse Proxy
app.MapReverseProxy();

// app.MapControllers();

app.Run();
