using Atraccion.Microservicios.Auth.Business.DTOs;
using Atraccion.Microservicios.Auth.Business.Interfaces;
using Atraccion.Microservicios.Auth.Business.Services;
using Atraccion.Microservicios.Auth.DataAccess.Context;
using Atraccion.Microservicios.Auth.DataAccess.Queries;
using Atraccion.Microservicios.Auth.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Auth.DataAccess.Repositories;
using Atraccion.Microservicios.Auth.DataAccess.Repositories.Interfaces;
using Atraccion.Microservicios.Auth.DataManagement.Interfaces;
using Atraccion.Microservicios.Auth.DataManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace Atraccion.Microservicios.Auth.Api.Extensions
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
            services.AddScoped<IUsuarioDataService, UsuarioDataService>();

            // ===============================
            // QUERIES
            // ===============================
            services.AddScoped<IUsuarioQuery, UsuarioQuery>();

            // ===============================
            // REPOSITORIES
            // ===============================
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

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
            services.AddScoped<IUsuarioBusinessService, UsuarioBusinessService>();

            // ===============================
            // INTEGRATIONS (gRPC)
            // ===============================
            services.AddGrpcClient<Atraccion.Microservicios.Auth.DataManagement.Protos.ClienteService.ClienteServiceClient>(o =>
            {
                o.Address = new Uri(configuration["GrpcUrls:Cliente:BaseUrl"] ?? "https://localhost:7229");
            });

            services.AddScoped<IClienteIntegration, Atraccion.Microservicios.Auth.DataManagement.Integrations.ClienteIntegrationGrpc>();

            return services;
        }
    }
}
