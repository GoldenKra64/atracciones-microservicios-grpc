using Atraccion.Microservicios.Atraccion.DataAccess.Common;
using Atraccion.Microservicios.Atraccion.DataAccess.Context;
using Atraccion.Microservicios.Atraccion.DataAccess.Queries.Filters;
using Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Queries
{
    public class AtraccionQuery : IAtraccionQuery
    {
        private readonly AtraccionesDbContext _context;

        public AtraccionQuery(AtraccionesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Atraccion.DataAccess.Entities.Atraccion?>> GetAtraccionTypeAsync()
        {
            return await _context.Atracciones.ToListAsync();
        }
        public async Task<Atraccion.DataAccess.Entities.Atraccion?> GetByIdAsync(string id)
        {
            return await _context.Atracciones
                .Include(a => a.Destino)
                .Include(a => a.Imagenes)
                .Include(a => a.CategoriaAtracciones)
                    .ThenInclude(c => c.Categoria)
                .Include(a => a.IdiomaAtracciones)
                    .ThenInclude(ia => ia.Idioma)
                .Include(a => a.IncluyeAtracciones)
                    .ThenInclude(ia => ia.Incluye)
                .Include(a => a.NoIncluyeAtracciones)
                    .ThenInclude(ia => ia.NoIncluye)
                .Include(a => a.TagAtracciones)
                    .ThenInclude(ta => ta.Tag)
                .Include(h => h.Horario)
                    .ThenInclude(t => t.Ticket)
                .Include(r => r.Resena)
                .FirstOrDefaultAsync(x => x.AtGuid == id);
        }
        public async Task<PagedResult<Atraccion.DataAccess.Entities.Atraccion>> SearchAsync(AtraccionFilterModel filter)
        {
            var query = _context.Atracciones
                .AsNoTracking()
                .Include(x => x.Destino)
                .Include(x => x.Imagenes)
                .Include(x => x.CategoriaAtracciones).ThenInclude(ca => ca.Categoria)
                .Include(x => x.IdiomaAtracciones).ThenInclude(ia => ia.Idioma)
                .Include(x => x.IncluyeAtracciones).ThenInclude(ia => ia.Incluye)
                .Where(x => x.AtEstado == "ACT")
                .AsQueryable();

            // 🔍 Nombre
            if (!string.IsNullOrWhiteSpace(filter.Nombre))
            {
                query = query.Where(x => x.AtNombre.Contains(filter.Nombre));
            }

            // 📍 Destino
            if (filter.DestinoId.HasValue)
            {
                query = query.Where(x => x.DesId == filter.DestinoId);
            }

            // 🏷️ Categorías
            if (filter.CategoriaIds != null && filter.CategoriaIds.Any())
            {
                query = query.Where(x =>
                    x.CategoriaAtracciones.Any(ca => filter.CategoriaIds.Contains(ca.CatId)));
            }

            // 🌐 Idiomas
            if (filter.IdiomaIds != null && filter.IdiomaIds.Any())
            {
                query = query.Where(x =>
                    x.IdiomaAtracciones.Any(ia => filter.IdiomaIds.Contains(ia.IdId)));
            }

            // 💰 Precio
            if (filter.PrecioMin.HasValue)
            {
                query = query.Where(x => x.AtPrecioReferencia >= filter.PrecioMin);
            }

            if (filter.PrecioMax.HasValue)
            {
                query = query.Where(x => x.AtPrecioReferencia <= filter.PrecioMax);
            }

            // 🚐 Transporte
            if (filter.IncluyeTransporte.HasValue)
            {
                query = query.Where(x => x.AtIncluyeTransporte == filter.IncluyeTransporte);
            }

            // 📊 Total
            var totalRecords = await query.CountAsync();

            // 📄 Paginación
            var items = await query
                .OrderBy(x => x.AtNombre)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Atraccion.DataAccess.Entities.Atraccion>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }

        public async Task<PagedResult<Atraccion.DataAccess.Entities.Atraccion>> GetPagedAsync(int pageNumber, int pageSize, string? ciudad, string? idioma, string? ordenarPor, decimal? calificacionMin, string? horario, string? tipo, string? subTipo)
        {
            var query = _context.Atracciones
                .Include(a => a.Destino)
                .Include(a => a.Imagenes)
                .Include(a => a.CategoriaAtracciones)
                    .ThenInclude(ca => ca.Categoria)
                .Include(a => a.IncluyeAtracciones)
                    .ThenInclude(ia => ia.Atraccion)
                .Include(a => a.NoIncluyeAtracciones)
                    .ThenInclude(ia => ia.Atraccion)
                .Include(a => a.TagAtracciones)
                    .ThenInclude(ta => ta.Tag)
                .Include(a => a.IdiomaAtracciones)
                    .ThenInclude(ia => ia.Idioma)
                .Include(a => a.Horario)
                .Include(r => r.Resena)
                .Where(a => a.AtEstado == "ACT")
                .AsQueryable();


            // Filtros
            if (ciudad != null)
            {
                query = query.Where(x => x.Destino.DesNombre.ToLower() == ciudad);
            }
            if (idioma != null)
            {
                query = query.Where(x => x.IdiomaAtracciones.Any(ia => ia.Idioma.IdNombre == idioma));
            }
            if (ordenarPor != null) /* Trending, Lowest Price, highest_weighted_rating */
            {
                switch (ordenarPor.ToLower())
                {
                    case "highest_weighted_rating":
                        query = query.OrderByDescending(x => x.Resena.Select(r => r.ResenaCalificacion).DefaultIfEmpty().Average());
                        break;
                    case "lowest_price":
                        query = query.OrderBy(x => x.AtPrecioReferencia);
                        break;
                    default: // trending
                        query = query.OrderBy(x => x.AtNombre);
                        break;
                }
            }

            if (calificacionMin.HasValue)
            {
                query = query.Where(x => x.Resena.Select(r => r.ResenaCalificacion).DefaultIfEmpty().Min() >= calificacionMin.Value);
            }

            if (horario != null)    /* 20:00:00-21:00:00*/
            {
                var horIni = TimeSpan.Parse(horario.Split('-')[0]);
                var horFin = TimeSpan.Parse(horario.Split('-')[1]);

                query = query.Where(x => x.Horario.Any(h => h.HorHoraInicio >= horIni &&
                                                             h.HorHoraFin <= horFin));
            }

            if (tipo != null) // Categoria
            {
                query = query.Where(x => x.CategoriaAtracciones.Any(ca => ca.Categoria.CatNombre == tipo.ToLower()));
            }

            if (subTipo != null) // Tag
            {
                query = query.Where(x => x.TagAtracciones.Any(ta => ta.Tag.TagDescription == subTipo.ToLower()));
            }

            // 📊 Total
            var totalRecords = await query.CountAsync();

            // 📄 Paginación
            var items = await query
                .OrderBy(x => x.AtNombre)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Atraccion.DataAccess.Entities.Atraccion>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Atraccion.DataAccess.Entities.Atraccion?> GetInternalByIdAsync(string id)
        {
            return await _context.Atracciones
                .Include(a => a.Destino)
                .Include(a => a.CategoriaAtracciones)
                    .ThenInclude(c => c.Categoria)
                .Include(a => a.IdiomaAtracciones)
                    .ThenInclude(ia => ia.Idioma)
                .Include(a => a.IncluyeAtracciones)
                    .ThenInclude(ia => ia.Incluye)
                .Include(a => a.NoIncluyeAtracciones)
                    .ThenInclude(ia => ia.NoIncluye)
                .Include(a => a.TagAtracciones)
                    .ThenInclude(ta => ta.Tag)
                 .Where(c => c.AtGuid == id).FirstOrDefaultAsync();
        }

        public async Task<int> GetActiveCountAsync()
        {
            return await _context.Atracciones.CountAsync(a => a.AtEstado == "ACT");
        }

        public async Task<List<Atraccion.DataAccess.Entities.Atraccion?>> GetAllInternalAsync()
        {
            return await _context.Atracciones
                .Include(a => a.Destino)
                .Include(a => a.CategoriaAtracciones)
                    .ThenInclude(c => c.Categoria)
                .Include(a => a.IdiomaAtracciones)
                    .ThenInclude(ia => ia.Idioma)
                .Include(a => a.IncluyeAtracciones)
                    .ThenInclude(ia => ia.Incluye)
                .Include(a => a.NoIncluyeAtracciones)
                    .ThenInclude(ia => ia.NoIncluye)
                .Include(a => a.TagAtracciones)
                    .ThenInclude(ta => ta.Tag)
                 .Where(c => c.AtEstado == "ACT").ToListAsync();
        }
    }
}
