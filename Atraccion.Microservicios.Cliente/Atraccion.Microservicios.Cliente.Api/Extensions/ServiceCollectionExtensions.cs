using Atraccion.Microservicios.Cliente.Business.DTOs;
using Atraccion.Microservicios.Cliente.Business.Interfaces;
using Atraccion.Microservicios.Cliente.Business.Services;
using Atraccion.Microservicios.Cliente.DataAccess.Context;
using Atraccion.Microservicios.Cliente.DataAccess.Queries;
using Atraccion.Microservicios.Cliente.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Cliente.DataAccess.Repositories;
using Atraccion.Microservicios.Cliente.DataAccess.Repositories.Interfaces;
using Atraccion.Microservicios.Cliente.DataManagement.Interfaces;
using Atraccion.Microservicios.Cliente.DataManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace Atraccion.Microservicios.Cliente.Api.Extensions
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
            services.AddScoped<IClienteDataService, ClienteDataService>();

            // ===============================
            // QUERIES
            // ===============================
            services.AddScoped<IClienteQuery, ClienteQuery>();

            // ===============================
            // REPOSITORIES
            // ===============================
            services.AddScoped<IClienteRepository, ClienteRepository>();

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
            services.AddScoped<IClienteBusinessService, ClienteBusinessService>();


            return services;
        }
    }
}
