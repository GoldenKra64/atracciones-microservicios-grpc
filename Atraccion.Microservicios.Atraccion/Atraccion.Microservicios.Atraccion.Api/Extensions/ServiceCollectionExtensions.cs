using Atraccion.Microservicios.Atraccion.Business.DTOs;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Atraccion.Microservicios.Atraccion.Business.Services;
using Atraccion.Microservicios.Atraccion.DataAccess.Context;
using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataAccess.Queries;
using Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Atraccion.DataAccess.Repositories;
using Atraccion.Microservicios.Atraccion.DataAccess.Repositories.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace Atraccion.Microservicios.Atraccion.Api.Extensions
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
            services.AddScoped<IAtraccionDataService, AtraccionDataService>();
            services.AddScoped<IDestinoDataService, DestinoDataService>();
            services.AddScoped<ICategoriaDataService, CategoriaDataService>();
            services.AddScoped<IIdiomaDataService, IdiomaDataService>();
            services.AddScoped<IImagenDataService, ImagenDataService>();
            services.AddScoped<IIncluyeDataService, IncluyeDataService>();
            services.AddScoped<INoIncluyeDataService, NoIncluyeDataService>();
            services.AddScoped<IResenaDataService, ResenaDataService>();
            services.AddScoped<ITicketDataService, TicketDataService>();
            services.AddScoped<IHorarioDataService, HorarioDataService>();
            services.AddScoped<ITagDataService, TagDataService>();

            // ===============================
            // QUERIES
            // ===============================
            services.AddScoped<IAtraccionQuery, AtraccionQuery>();
            services.AddScoped<IDestinoQuery, DestinoQuery>();
            services.AddScoped<ICategoriaQuery, CategoriaQuery>();
            services.AddScoped<IIdiomaQuery, IdiomaQuery>();
            services.AddScoped<IResenaQuery, ResenaQuery>();
            services.AddScoped<ITicketQuery, TicketQuery>();
            services.AddScoped<IHorarioQuery, HorarioQuery>();
            services.AddScoped<ITagQuery, TagQuery>();
            services.AddScoped<IImagenQuery, ImagenQuery>();

            // ===============================
            // REPOSITORIES
            // ===============================
            services.AddScoped<IAtraccionRepository, AtraccionRepository>();
            services.AddScoped<IDestinoRepository, DestinoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IImagenRepository, ImagenRepository>();
            services.AddScoped<IIncluyeRepository, IncluyeRepository>();
            services.AddScoped<INoIncluyeRepository, NoIncluyeRepository>();
            services.AddScoped<IResenaRepository, ResenaRepository>();
            services.AddScoped<IRepository<Idioma>, IdiomaRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IHorarioRepository, HorarioRepository>();
            // services.AddScoped<ITagRepository, TagRepository>();

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
            services.AddScoped<IAtraccionBusinessService, AtraccionBusinessService>();
            services.AddScoped<IDestinoBusinessService, DestinoBusinessService>();
            services.AddScoped<ICategoriaBusinessService, CategoriaBusinessService>();
            services.AddScoped<IIdiomaBusinessService, IdiomaBusinessService>();
            services.AddScoped<IImagenBusinessService, ImagenBusinessService>();
            services.AddScoped<IIncluyeBusinessService, IncluyeBusinessService>();
            services.AddScoped<INoIncluyeBusinessService, NoIncluyeBusinessService>();
            services.AddScoped<IResenaBusinessService, ResenaBusinessService>();
            services.AddScoped<ITicketBusinessService, TicketBusinessService>();
            services.AddScoped<IHorarioBusinessService, HorarioBusinessService>();
            services.AddScoped<ITagBusinessService, TagBusinessService>();


            return services;
        }
    }
}
