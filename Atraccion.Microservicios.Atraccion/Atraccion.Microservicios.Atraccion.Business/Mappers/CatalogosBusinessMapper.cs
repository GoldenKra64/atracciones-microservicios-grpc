using Atraccion.Microservicios.Atraccion.Business.DTOs.Categoria;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Idioma;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Tag;
using Atraccion.Microservicios.Atraccion.DataManagement.Models;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Categoria;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Mappers
{
    public static class CatalogosBusinessMapper
    {

        public static CategoriaResponse ToResponse(CategoriaModel model)
        {
            return new CategoriaResponse
            {
                Id = model.Id,
                Guid = model.Guid,
                Nombre = model.Nombre,
                Children = model.Children?.Select(ToResponse).ToList() ?? new()
            };
        }

        public static IdiomaResponse ToResponse(IdiomaModel model)
        {
            return new IdiomaResponse
            {
                Id = model.Id,
                Nombre = model.Nombre
            };
        }
        public static TagResponse ToResponse(TagModel model)
        {
            return new TagResponse
            {
                Id = model.Id,
                Nombre = model.Nombre
            };
        }
    }
}
