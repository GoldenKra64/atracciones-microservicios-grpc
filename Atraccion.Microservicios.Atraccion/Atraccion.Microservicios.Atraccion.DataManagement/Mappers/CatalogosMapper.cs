using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataManagement.Models;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Categoria;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Destino;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Incluye;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.NoIncluye;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Mappers
{
    public static class CatalogosMapper
    {
        public static DestinoModel ToModel(Destino entity)
        {
            return new DestinoModel
            {
                Id = entity.DesId,
                Guid = entity.DesGuid,
                Estado = entity.DesEstado,
                Nombre = entity.DesNombre,
                Pais = entity.DesPais,
                ImagenUrl = entity.DesImagenUrl
            };
        }

        public static CategoriaModel ToModel(Categoria entity)
        {
            return new CategoriaModel
            {
                Id = entity.CatId,
                Guid = entity.CatGuid,
                Estado = entity.CatEstado,
                Nombre = entity.CatNombre,
                ParentId = entity.CatParentId,
                Children = entity.Children?.Select(ToModel).ToList() ?? new()
            };
        }

        public static IdiomaModel ToModel(Idioma entity)
        {
            return new IdiomaModel
            {
                Id = entity.IdId,
                Nombre = entity.IdNombre
            };
        }

        public static IncluyeModel ToModel(Incluye entity)
        {
            return new IncluyeModel
            {
                Id = entity.IncId,
                Descripcion = entity.IncDescripcion
            };
        }

        public static NoIncluyeModel ToModel(NoIncluye entity)
        {
            return new NoIncluyeModel
            {
                Id = entity.NoIncId,
                Descripcion = entity.NoIncDescripcion
            };
        }

        public static TagModel ToModel(Tag entity)
        {
            return new TagModel
            {
                Id = entity.TagId,
                Nombre = entity.TagDescription
            };
        }
        public static void UpdateEntity(Categoria entity, CategoriaUpdateModel model)
        {
            entity.CatNombre = model.Nombre;
            entity.CatParentId = model.ParentId;
        }
        public static void UpdateEntity(Destino entity, DestinoUpdateModel model)
        {
            entity.DesNombre = model.Nombre;
            entity.DesPais = model.Pais;
            entity.DesImagenUrl = model.ImagenUrl;
        }
        public static void UpdateEntity(Incluye entity, IncluyeUpdateModel model)
        {
            entity.IncDescripcion = model.Descripcion;
        }
        public static void UpdateEntity(NoIncluye entity, NoIncluyeUpdateModel model)
        {
            entity.NoIncDescripcion = model.Descripcion;
        }
    }
}
