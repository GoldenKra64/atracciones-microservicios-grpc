using Atraccion.Microservicios.Reserva.Business.DTOs;
using Atraccion.Microservicios.Reserva.Business.Interfaces;
using Atraccion.Microservicios.Reserva.Business.Services;
using Atraccion.Microservicios.Reserva.DataAccess.Context;
using Atraccion.Microservicios.Reserva.DataAccess.Queries;
using Atraccion.Microservicios.Reserva.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Reserva.DataAccess.Repositories;
using Atraccion.Microservicios.Reserva.DataAccess.Repositories.Interfaces;
using Atraccion.Microservicios.Reserva.DataManagement.Interfaces;
using Atraccion.Microservicios.Reserva.DataManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace Atraccion.Microservicios.Reserva.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // ===============================
            // DB CONTEXT
            // ===============================
            services.AddDbContext<AtraccionesDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("AtraccionesDbPG")));

            // ===============================
            // DATA SERVICES
            // ===============================
            services.AddScoped<IReservaDataService, ReservaDataService>();

            // ===============================
            // QUERIES
            // ===============================
            services.AddScoped<IReservaQuery, ReservaQuery>();

            // ===============================
            // REPOSITORIES
            // ===============================
            services.AddScoped<IReservaRepository, ReservaRepository>();

            // ===============================
            // UNIT OF WORK
            // ===============================
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            // ===============================
            // UNIT OF WORK
            // ===============================
            services.AddScoped<JwtSettings>();

            // ===============================
            // SERVICES (BUSINESS)
            // ===============================
            services.AddScoped<IReservaBusinessService, ReservaBusinessService>();

            // ===============================
            // INTEGRATIONS (gRPC)
            // ===============================
            services.AddGrpcClient<Atraccion.Microservicios.Reserva.DataManagement.Protos.AtraccionService.AtraccionServiceClient>(o =>
            {
                o.Address = new Uri(configuration["GrpcUrls:Atraccion:BaseUrl"] ?? "https://localhost:7143");
            });

            services.AddGrpcClient<Atraccion.Microservicios.Reserva.DataManagement.Protos.FacturaService.FacturaServiceClient>(o =>
            {
                o.Address = new Uri(configuration["GrpcUrls:Factura:BaseUrl"] ?? "https://localhost:7289");
            });

            services.AddScoped<IAtraccionIntegration, Atraccion.Microservicios.Reserva.DataManagement.Integrations.AtraccionIntegrationGrpc>();
            services.AddScoped<IFacturaIntegration, Atraccion.Microservicios.Reserva.DataManagement.Integrations.FacturaIntegrationGrpc>();

            return services;
        }
    }
}
