using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Atraccion;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Horario;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Imagen;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Resena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Mappers
{
    public static class AtraccionMapper
    {
        public static AtraccionModel ToModel(Atraccion.DataAccess.Entities.Atraccion entity)
        {
            return new AtraccionModel
            {
                Id = entity.AtId,
                Guid = entity.AtGuid,
                Estado = entity.AtEstado,

                Nombre = entity.AtNombre,
                Descripcion = entity.AtDescripcion,
                Direccion = entity.AtDireccion,
                DuracionMinutos = entity.AtDuracionMinutos,
                PrecioReferencia = entity.AtPrecioReferencia,

                IncluyeAcompaniante = entity.AtIncluyeAcompaniante,
                IncluyeTransporte = entity.AtIncluyeTransporte,
                PuntoEncuentro = entity.AtPuntoEncuentro,
                Moneda = entity.AtMoneda,

                Destino = entity.Destino != null
                    ? CatalogosMapper.ToModel(entity.Destino)
                    : null!,

                Imagenes = entity.Imagenes?
                    .Select(i => new ImagenModel
                    {
                        Id = i.ImgId,
                        Url = i.ImgUrl,
                        Descripcion = i.ImgDescripcion
                    }).ToList() ?? new(),

                Categorias = entity.CategoriaAtracciones?
                    .Select(ca => CatalogosMapper.ToModel(ca.Categoria))
                    .ToList() ?? new(),

                Idiomas = entity.IdiomaAtracciones?
                    .Select(ia => CatalogosMapper.ToModel(ia.Idioma))
                    .ToList() ?? new(),

                Incluyes = entity.IncluyeAtracciones?
                    .Where(ia => ia.Incluye != null)
                    .Select(ia => CatalogosMapper.ToModel(ia.Incluye))
                    .ToList() ?? new(),

                NoIncluyes = entity.NoIncluyeAtracciones?
                    .Where(ia => ia.NoIncluye != null)
                    .Select(ia => CatalogosMapper.ToModel(ia.NoIncluye))
                    .ToList() ?? new(),

                Horarios = entity.Horario?
                    .Select(ia => new HorarioModel
                    {
                        HorarioId = ia.HorId,
                        HorarioGuid = ia.HorGuid,
                        AtraccionId = ia.AtId,
                        Fecha = ia.HorFecha.ToString("yyyy-MM-dd"),
                        HoraInicio = ia.HorHoraInicio.ToString(@"hh\:mm"),
                        HoraFin = ia.HorHoraFin?.ToString(@"hh\:mm"),
                        Cupos = ia.HorCuposDisponibles,
                        Tickets = ia.Ticket?.Select(TicketMapper.ToModel).ToList() ?? new()
                    }).ToList() ?? new(),

                TagAtracciones = entity.TagAtracciones?
                    .Where(ta => ta.Tag != null)
                    .Select(ta => CatalogosMapper.ToModel(ta.Tag))
                    .ToList() ?? new(),

                Resena = entity.Resena?
                    .Select(r => new ResenaModel
                    {
                        AtraccionId = r.AtId,
                        Comentario = r.ResenaComentario,
                        Calificacion = r.ResenaCalificacion,
                        Fecha = r.ResenaFechaCreacion.ToShortDateString(),
                        ClienteId = r.CliId
                    }).ToList() ?? new(),
            };
        }

        public static Atraccion.DataAccess.Entities.Atraccion ToEntity(AtraccionCreateModel model)
        {
            var entity = new Atraccion.DataAccess.Entities.Atraccion
            {
                DesId = model.DestinoId,
                AtNombre = model.Nombre,
                AtGuid = Guid.NewGuid().ToString(),
                AtDescripcion = model.Descripcion,
                AtDireccion = model.Direccion,
                AtDuracionMinutos = model.DuracionMinutos,
                AtPuntoEncuentro = model.PuntoEncuentro,
                AtPrecioReferencia = model.PrecioReferencia,
                AtMoneda = model.Moneda,
                AtIncluyeAcompaniante = model.IncluyeAcompaniante,
                AtIncluyeTransporte = model.IncluyeTransporte,
                AtFechaIngreso = DateTime.UtcNow,
                AtUsuarioIngreso = "system", // En un escenario real, esto debería ser el usuario autenticado
                AtIpIngreso = "127.0.0.1", // En un escenario real, esto debería ser la IP del cliente
                AtEstado = "ACT",
            };

            entity.CategoriaAtracciones = (model.CategoriaIds ?? Enumerable.Empty<int>()).Select(cateId => new CategoriaAtraccion { CatId = cateId, Atraccion = entity }).ToList();
            entity.IncluyeAtracciones = (model.IncluyeIds ?? Enumerable.Empty<int>()).Select(incId => new IncluyeAtraccion { IncId = incId, Atraccion = entity }).ToList();
            entity.IdiomaAtracciones = (model.IdiomaIds ?? Enumerable.Empty<int>()).Select(idiId => new IdiomaAtraccion { IdId = idiId, Atraccion = entity }).ToList();
            entity.NoIncluyeAtracciones = (model.NoIncluyeIds ?? Enumerable.Empty<int>()).Select(noIncId => new NoIncluyeAtraccion { NoIncId = noIncId, Atraccion = entity }).ToList();
            entity.TagAtracciones = (model.TagIds ?? Enumerable.Empty<int>()).Select(tagId => new TagAtraccion { TagId = tagId, Atraccion = entity }).ToList();

            return entity;
        }
        public static void UpdateEntity(Atraccion.DataAccess.Entities.Atraccion entity, AtraccionUpdateModel model)
        {
            entity.DesId = model.DestinoId;
            entity.AtNombre = model.Nombre;
            entity.AtDescripcion = model.Descripcion;
            entity.AtPrecioReferencia = model.PrecioReferencia;
            entity.AtMoneda = model.Moneda;
            entity.AtDuracionMinutos = model.DuracionMinutos;
            entity.AtDireccion = model.Direccion;

            entity.AtIncluyeAcompaniante = model.IncluyeAcompaniante;
            entity.AtIncluyeTransporte = model.IncluyeTransporte;

            // Categoria
            var nuevosCatIds = model.CategoriaIds ?? Enumerable.Empty<int>();

            entity.CategoriaAtracciones
                .Where(x => !nuevosCatIds.Contains(x.CatId))
                .ToList()
                .ForEach(x => entity.CategoriaAtracciones.Remove(x));

            var existentes = entity.CategoriaAtracciones.Select(x => x.CatId).ToHashSet();

            foreach (var id in nuevosCatIds)
            {
                if (!existentes.Contains(id))
                {
                    entity.CategoriaAtracciones.Add(new CategoriaAtraccion
                    {
                        CatId = id,
                        AtId = entity.AtId
                    });
                }
            }

            // Idioma
            var nuevosIds = model.IdiomaIds ?? Enumerable.Empty<int>();

            entity.IdiomaAtracciones
                .Where(x => !nuevosIds.Contains(x.IdId))
                .ToList()
                .ForEach(x => entity.IdiomaAtracciones.Remove(x));

            var existentes_id = entity.IdiomaAtracciones.Select(x => x.IdId).ToHashSet();

            foreach (var id in nuevosIds)
            {
                if (!existentes_id.Contains(id))
                {
                    entity.IdiomaAtracciones.Add(new IdiomaAtraccion
                    {
                        IdId = id,
                        AtId = entity.AtId
                    });
                }
            }

            // Incluye
            
            var nuevosIncIds = model.IncluyeIds ?? Enumerable.Empty<int>();

            entity.IncluyeAtracciones
                .Where(x => !nuevosIncIds.Contains(x.IncId))
                .ToList()
                .ForEach(x => entity.IncluyeAtracciones.Remove(x));

            var existentes_inc = entity.IncluyeAtracciones.Select(x => x.IncId).ToHashSet();

            foreach (var id in nuevosIncIds)
            {
                if (!existentes_inc.Contains(id))
                {
                    entity.IncluyeAtracciones.Add(new IncluyeAtraccion
                    {
                        IncId = id,
                        AtId = entity.AtId
                    });
                }
            }

            // No Incluye
            var nuevosNoIncIds = model.NoIncluyeIds ?? Enumerable.Empty<int>();

            entity.NoIncluyeAtracciones
                .Where(x => !nuevosNoIncIds.Contains(x.NoIncId))
                .ToList()
                .ForEach(x => entity.NoIncluyeAtracciones.Remove(x));

            var existentes_noinc = entity.NoIncluyeAtracciones.Select(x => x.NoIncId).ToHashSet();

            foreach (var id in nuevosNoIncIds)
            {
                if (!existentes_noinc.Contains(id))
                {
                    entity.NoIncluyeAtracciones.Add(new NoIncluyeAtraccion
                    {
                        NoIncId = id,
                        AtId = entity.AtId
                    });
                }
            }

            // Tag
            var nuevosTagIds = model.TagIds ?? Enumerable.Empty<int>();

            entity.TagAtracciones
                .Where(x => !nuevosTagIds.Contains(x.TagId))
                .ToList()
                .ForEach(x => entity.TagAtracciones.Remove(x));

            var existentes_tag = entity.TagAtracciones.Select(x => x.TagId).ToHashSet();

            foreach (var id in nuevosTagIds)
            {
                if (!existentes_tag.Contains(id))
                {
                    entity.TagAtracciones.Add(new TagAtraccion
                    {
                        TagId = id,
                        AtId = entity.AtId
                    });
                }
            }

            entity.CategoriaAtracciones = (model.CategoriaIds ?? Enumerable.Empty<int>())
                .Select(cateId => new CategoriaAtraccion { CatId = cateId, AtId = entity.AtId })
                .ToList();
            entity.IncluyeAtracciones = (model.IncluyeIds ?? Enumerable.Empty<int>())
                .Select(incId => new IncluyeAtraccion { IncId = incId, AtId = entity.AtId })
                .ToList();

            entity.IdiomaAtracciones = (model.IdiomaIds ?? Enumerable.Empty<int>())
                .Select(idiId => new IdiomaAtraccion { IdId = idiId, AtId = entity.AtId })
                .ToList();

            entity.NoIncluyeAtracciones = (model.NoIncluyeIds ?? Enumerable.Empty<int>())
                .Select(noIncId => new NoIncluyeAtraccion { NoIncId = noIncId, AtId = entity.AtId })
                .ToList();

            entity.TagAtracciones = (model.TagIds ?? Enumerable.Empty<int>())
                .Select(entityId => new TagAtraccion { TagId = entityId, AtId = entity.AtId }).ToList();
        }

        public static AtraccionTypeModel ToTypeModel(Atraccion.DataAccess.Entities.Atraccion atraccion)
        {
            return new AtraccionTypeModel
            {
                Id = atraccion.AtId,
                Guid = atraccion.AtGuid,
                Nombre = atraccion.AtNombre
            };
        }
    }
}
