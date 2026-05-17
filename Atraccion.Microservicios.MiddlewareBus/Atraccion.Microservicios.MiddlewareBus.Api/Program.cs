using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .ConfigureHttpClient((context, handler) =>
    {
        // Disable automatic redirects so YARP forwards them back to the client
        // unchanged. Without this, 301/302 redirects (e.g. HTTP→HTTPS) cause
        // the HttpClient to re-issue the request as GET, producing 405 errors
        // for POST/PUT/DELETE endpoints.
        handler.AllowAutoRedirect = false;
    });

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto
});


app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapReverseProxy();

app.Run();
