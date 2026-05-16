using Atraccion.Microservicios.Factura.Business.DTOs;
using Atraccion.Microservicios.Factura.Business.Interfaces;
using Atraccion.Microservicios.Factura.Business.Services;
using Atraccion.Microservicios.Factura.DataAccess.Context;
using Atraccion.Microservicios.Factura.DataAccess.Queries;
using Atraccion.Microservicios.Factura.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Factura.DataAccess.Repositories;
using Atraccion.Microservicios.Factura.DataAccess.Repositories.Interfaces;
using Atraccion.Microservicios.Factura.DataManagement.Interfaces;
using Atraccion.Microservicios.Factura.DataManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace Atraccion.Microservicios.Factura.Api.Extensions
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
            services.AddScoped<IFacturaDataService, FacturaDataService>();

            // ===============================
            // QUERIES
            // ===============================
            services.AddScoped<IFacturaQuery, FacturaQuery>();

            // ===============================
            // REPOSITORIES
            // ===============================
            services.AddScoped<IFacturaRepository, FacturaRepository>();


            // ===============================
            // UNIT OF WORK
            // ===============================
            services.AddScoped<JwtSettings>();

            // ===============================
            // SERVICES (BUSINESS)
            // ===============================
            services.AddScoped<IFacturaBusinessService, FacturaBusinessService>();


            // ===============================
            // GRPC CLIENTS
            // ===============================
            services.AddGrpcClient<Atraccion.Microservicios.Factura.DataManagement.Protos.ClienteService.ClienteServiceClient>(o =>
            {
                o.Address = new Uri(configuration["GrpcUrls:ClienteUrl:BaseUrl"] ?? "https://localhost:7119");
            });

            return services;
        }
    }
}
