using Atraccion.Microservicios.Atraccion.Api.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atraccion.Microservicios.Atraccion.Api.Extensions
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection AddRabbitMqMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumer<ReduceCuposConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMQ:HostName"] ?? "localhost", configuration["RabbitMQ:VirtualHost"], h =>
                    {
                        h.Username(configuration["RabbitMQ:UserName"] ?? "guest");
                        h.Password(configuration["RabbitMQ:Password"] ?? "guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
