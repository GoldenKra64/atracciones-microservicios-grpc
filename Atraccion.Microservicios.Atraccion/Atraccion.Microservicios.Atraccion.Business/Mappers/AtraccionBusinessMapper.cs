using Atraccion.Microservicios.Atraccion.Business.DTOs;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataManagement.Models;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Atraccion;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Horario;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Resena;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Ticket;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Mappers
{
    public static class AtraccionBusinessMapper
    {
        // 🔹 Request → Model
        public static AtraccionCreateModel ToCreateModel(CreateAtraccionRequest request)
        {
            return new AtraccionCreateModel
            {
                DestinoId = request.DestinoId,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                PrecioReferencia = request.PrecioReferencia,
                IncluyeAcompaniante = request.IncluyeAcompaniante,
                IncluyeTransporte = request.IncluyeTransporte,
                Moneda = request.Moneda ?? "USD",
                Direccion = request.Direccion,
                PuntoEncuentro = request.PuntoEncuentro,
                DuracionMinutos = request.DuracionMinutos,

                CategoriaIds = request.CategoriaIds,
                IdiomaIds = request.IdiomaIds,
                IncluyeIds = request.IncluyeIds,
                NoIncluyeIds = request.NoIncluyeIds,
                TagIds = request.TagIds,
                // ImageIds = request.ImageIds,
                // HorarioIds = request.HorarioIds
            };
        }

        public static AtraccionUpdateModel ToUpdateModel(UpdateAtraccionRequest request)
        {
            return new AtraccionUpdateModel
            {
                Id = request.Id,
                DestinoId = request.DestinoId,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                PrecioReferencia = request.PrecioReferencia,
                DuracionMinutos = request.DuracionMinutos,
                IncluyeAcompaniante = request.IncluyeAcompaniante,
                IncluyeTransporte = request.IncluyeTransporte,
                Moneda = request.Moneda ?? "USD",
                Direccion = request.Direccion,
                PuntoEncuentro = request.PuntoEncuentro,

                CategoriaIds = request.CategoriaIds,
                IdiomaIds = request.IdiomaIds,
                IncluyeIds = request.IncluyeIds,
                NoIncluyeIds = request.NoIncluyeIds,
                TagIds = request.TagIds
                // ImageIds = request.ImageIds,
                // HorarioIds = request.HorarioIds
            };
        }

        // 🔹 Model → Response

        public static AtraccionResponse ToAtraccionResponse(AtraccionModel model)
        {
            return new AtraccionResponse
            {
                Id = model.Id,
                Guid = model.Guid,
                Nombre = model.Nombre,
                Direccion = model.Direccion,
                DuracionMinutos = model.DuracionMinutos,
                Moneda = model.Moneda ?? "USD",
                PuntoEncuentro = model.PuntoEncuentro,
                Descripcion = model.Descripcion,
                PrecioReferencia = (double)model.PrecioReferencia,
                IncluyeAcompaniante = model.IncluyeAcompaniante,
                IncluyeTransporte = model.IncluyeTransporte,

                Destino = DestinoBusinessMapper.ToResponse(model.Destino),

                Categorias = model.Categorias
                    .Select(CatalogosBusinessMapper.ToResponse)
                    .ToList(),

                Idiomas = model.Idiomas
                    .Select(CatalogosBusinessMapper.ToResponse)
                    .ToList(),
                
                Incluyes = model.Incluyes
                    .Select(IncluyeBusinessMapper.ToResponse)
                    .ToList(),

                NoIncluyes = model.NoIncluyes
                    .Select(NoIncluyeBusinessMapper.ToResponse)
                    .ToList(),
                
                TagAtracciones = model.TagAtracciones
                    .Select(CatalogosBusinessMapper.ToResponse)
                    .ToList()

            };
        }
        public static ListadoAtracciones ToResponse(AtraccionModel model)
        {
            
            var destinoNombre = model.Destino?.Nombre ?? string.Empty;
            var destinoPais = model.Destino?.Pais ?? string.Empty;

            var resenas = model.Resena ?? new List<ResenaModel>();

            var totalResenias = resenas.Count;
            var calificacion = totalResenias > 0 ? resenas.Average(r => r.Calificacion) : 0.0;

            var horarios = model.Horarios ?? new List<HorarioModel>();
            var cuposDisponibles = horarios.Sum(h => h?.Cupos ?? 0);
            var disponible = horarios.Any(h => (h?.Cupos ?? 0) > 0);

            DateTime nowDate = DateTime.UtcNow.Date;


            var proximaFecha = horarios
                .Where(h => (h?.Cupos ?? 0) > 0)
                .Select(h =>
                {
                    if (DateTime.TryParse(h?.Fecha, out var dt)) return (DateTime?)dt;
                    return null;
                })
                .Where(d => d.HasValue && d.Value >= nowDate)
                .OrderBy(d => d)
                .Select(d => d.Value.ToString("yyyy-MM-dd"))
                .FirstOrDefault();

            
            var parentCat = model.Categorias?.FirstOrDefault(c => c.ParentId == null);
            var childCat = model.Categorias?.FirstOrDefault(c => c.ParentId != null);
            
            var tipoNombre = parentCat?.Nombre ?? string.Empty;
            var tipoTagname = parentCat?.Guid ?? string.Empty;

            var subtipoNombre = childCat?.Nombre;
            var subtipoTagname = childCat?.Guid;

            var etiquetas = model.TagAtracciones?
                .Select(i => i.Nombre)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList() ?? new List<string>();

            var imagenPrincipal = model.Imagenes?.FirstOrDefault(i => !string.IsNullOrEmpty(i.Url))?.Url;
            
            var descCorta = model.Descripcion ?? string.Empty;
            if (descCorta.Length > 150)
            {
                descCorta = descCorta.Substring(0, 147) + "...";
            }

            return new ListadoAtracciones
            {
                id = model.Guid ?? string.Empty,
                nombre = model.Nombre ?? string.Empty,
                descripcion_corta = descCorta,
                precio_desde = model.PrecioReferencia,
                moneda = model.Moneda ?? "USD",
                ciudad = destinoNombre,
                total_resenas = totalResenias,
                calificacion = calificacion,
                idiomas_disponibles = model.Idiomas?.Select(i => i.Nombre).Where(n => !string.IsNullOrWhiteSpace(n)).ToList() ?? new List<string>(),
                
                disponibilidad = new DisponibilidadDto
                {
                    disponible = disponible,
                    disponible_hoy = horarios.Any(h =>
                    {
                        if (h == null) return false;
                        if ((h.Cupos) <= 0) return false;
                        if (!DateTime.TryParse(h.Fecha, out var dt)) return false;
                        return dt == nowDate;
                    }),
                    proxima_fecha_disponible = proximaFecha ?? string.Empty,
                    cupos_disponibles = cuposDisponibles
                },
                
                tipo_nombre = tipoNombre,
                tipo_tagname = tipoTagname,
                subtipo_nombre = subtipoNombre,
                subtipo_tagname = subtipoTagname,
                pais = destinoPais,
                etiquetas = etiquetas,
                duracion_minutos = model.DuracionMinutos ?? 0,
                imagen_principal = imagenPrincipal,
                _links = new Dictionary<string, string>
                {
                    { "self", $"/api/v2/atracciones/{model.Guid}" }
                }
            };
        }
        public static FiltroModel ToFilterModel(FiltroDto request)
        {
            return new FiltroModel
            {
                Page = request.Page,
                Limit = request.Limit,
                Ciudad = request.Ciudad,
                Tipo = request.Tipo,
                Subtipo = request.Subtipo,
                Etiqueta = request.Etiqueta,
                Idioma = request.Idioma,
                CalificacionMin = request.CalificacionMin,
                Horario = request.Horario,
                Disponible = request.Disponible,
                OrdenarPor = request.OrdenarPor
            };
        }

        public static AtraccionDetalleDto ToResponseDetalle(AtraccionModel model)
        {
            var destinoNombre = model.Destino?.Nombre ?? string.Empty;
            var destinoPais = model.Destino?.Pais ?? string.Empty;

            var resenas = model.Resena ?? new List<ResenaModel>();
            var totalResenias = resenas.Count;
            var calificacion = totalResenias > 0 ? resenas.Average(r => r.Calificacion) : 0.0;

            var horarios = model.Horarios ?? new List<HorarioModel>();
            var cuposDisponibles = horarios.Sum(h => h?.Cupos ?? 0);
            var disponible = horarios.Any(h => (h?.Cupos ?? 0) > 0);

            DateTime nowDate = DateTime.UtcNow.Date;

            var proximaFecha = horarios
                .Where(h => (h?.Cupos ?? 0) > 0)
                .Select(h =>
                {
                    if (DateTime.TryParse(h?.Fecha, out var dt)) return (DateTime?)dt;
                    return null;
                })
                .Where(d => d.HasValue && d.Value >= nowDate)
                .OrderBy(d => d)
                .Select(d => d.Value.ToString("yyyy-MM-dd"))
                .FirstOrDefault();
            
            var parentCat = model.Categorias?.FirstOrDefault(c => c.ParentId == null);
            var childCat = model.Categorias?.FirstOrDefault(c => c.ParentId != null);
            
            var tipoNombre = parentCat?.Nombre ?? string.Empty;
            var tipoTagname = parentCat?.Guid ?? string.Empty;

            var subtipoNombre = childCat?.Nombre ?? string.Empty;
            var subtipoTagname = childCat?.Guid ?? string.Empty;

            var etiquetas = model.TagAtracciones?
                .Select(i => i.Nombre)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList() ?? new List<string>();

            var imagenPrincipal = model.Imagenes?.FirstOrDefault(i => !string.IsNullOrEmpty(i.Url))?.Url ?? string.Empty;
            
            var descCorta = model.Descripcion ?? string.Empty;
            if (descCorta.Length > 150)
            {
                descCorta = descCorta.Substring(0, 147) + "...";
            }

            return new AtraccionDetalleDto
            {
                id = model.Guid ?? string.Empty,
                nombre = model.Nombre ?? string.Empty,
                ciudad = destinoNombre,
                pais = destinoPais,
                tipo_tagname = tipoTagname,
                tipo_nombre = tipoNombre,
                subtipo_tagname = subtipoTagname,
                subtipo_nombre = subtipoNombre,
                etiquetas = etiquetas,
                descripcion_corta = descCorta,
                imagen_principal = imagenPrincipal,
                duracion_minutos = model.DuracionMinutos ?? 0,
                precio_desde = (decimal)(model.PrecioReferencia ?? 0),
                moneda = model.Moneda ?? "USD",
                calificacion = calificacion,
                total_resenas = totalResenias,
                idiomas_disponibles = model.Idiomas?.Select(i => i.Nombre).Where(n => !string.IsNullOrWhiteSpace(n)).ToList() ?? new List<string>(),
                
                disponibilidad = new DisponibilidadDto
                {
                    disponible = disponible,
                    disponible_hoy = horarios.Any(h =>
                    {
                        if (h == null) return false;
                        if ((h.Cupos) <= 0) return false;
                        if (!DateTime.TryParse(h.Fecha, out var dt)) return false;
                        return dt == nowDate;
                    }),
                    proxima_fecha_disponible = proximaFecha ?? string.Empty,
                    cupos_disponibles = cuposDisponibles
                },

                descripcion = model.Descripcion ?? string.Empty,
                incluye_transporte = model.IncluyeTransporte,
                incluye_acompaniante = model.IncluyeAcompaniante,
                punto_encuentro = model.PuntoEncuentro,
                imagenes = model.Imagenes?.Select(i => i.Url).Where(url => !string.IsNullOrEmpty(url)).ToList() ?? new List<string>(),
                incluye = model.Incluyes?.Select(i => i.Descripcion).Where(s => !string.IsNullOrWhiteSpace(s)).ToList() ?? new List<string>(),
                no_incluye = model.NoIncluyes?.Select(i => i.Descripcion).Where(s => !string.IsNullOrWhiteSpace(s)).ToList() ?? new List<string>(),
                horarios_proximos = model.Horarios?.Select(h => new HorarioDto
                {
                    AtraccionId = h.AtraccionId,
                    HorarioId = h.HorarioId,
                    HorarioGuid = h.HorarioGuid,
                    Cupos = h.Cupos,
                    Fecha = h.Fecha,
                    HoraInicio = h.HoraInicio,
                    HoraFin = h.HoraFin
                }).ToList() ?? new List<HorarioDto>(),
                tickets = model.Horarios?.SelectMany(h => h.Tickets ?? new List<TicketModel>())
                    .Select(t => new TicketDto
                    {
                        HorId = t.HorarioId,
                        TckGuid = t.Guid,
                        Tipo = t.Tipo,
                        Precio = t.Precio,
                        Moneda = model.Moneda ?? "USD"
                    }).ToList() ?? new List<TicketDto>(),
                
                Links = new LinksDto
                {
                    Self = $"/api/v2/atracciones/{model.Guid}"
                }
            };
        }
        public static AtraccionTypeResponse ToModelType(AtraccionTypeModel model)
        {
            return new AtraccionTypeResponse
            {
                Id = model.Id,
                Name = model.Nombre
            };
        }

        public static List<TicketDto> MapTicketsToResponse(AtraccionModel model)
        {
            return model.Horarios?.SelectMany(h => h.Tickets ?? new List<TicketModel>())
                .Select(t => new TicketDto
                {
                    HorId = t.HorarioId,
                    TckGuid = t.Guid,
                    Tipo = t.Tipo,
                    Precio = t.Precio,
                    Moneda = model.Moneda ?? "USD"
                }).ToList() ?? new List<TicketDto>();
        }

        public static List<TicketDto> MapTicketsByHorarioToResponse(AtraccionModel model, string horarioId)
        {
            var horario = model.Horarios?.FirstOrDefault(h => h.HorarioGuid == horarioId);
            if (horario == null) return new List<TicketDto>();

            return horario.Tickets?.Select(t => new TicketDto
            {
                HorId = t.HorarioId,
                TckGuid = t.Guid,
                Tipo = t.Tipo,
                Precio = t.Precio,
                Moneda = model.Moneda ?? "USD"
            }).ToList() ?? new List<TicketDto>();
        }

        public static List<HorarioDto> MapHorariosToResponse(AtraccionModel model)
        {
            return model.Horarios?.Select(h => new HorarioDto
            {
                AtraccionId = h.AtraccionId,
                HorarioId = h.HorarioId,
                HorarioGuid = h.HorarioGuid,
                Cupos = h.Cupos,
                Fecha = h.Fecha,
                HoraInicio = h.HoraInicio,
                HoraFin = h.HoraFin
            }).ToList() ?? new List<HorarioDto>();
        }
    }
}
