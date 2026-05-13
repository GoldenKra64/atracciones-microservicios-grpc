using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Imagen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Mappers
{
    public static class ImagenMapper
    {
        public static ImagenModel ToModel(Imagen entity)
        {
            return new ImagenModel
            {
                Id = entity.ImgId,
                Descripcion = entity.ImgDescripcion,
                Url = entity.ImgUrl,
                AtraccionId = entity.AtId
            };
        }

        public static Imagen ToEntity(ImagenCreateModel model)
        {
            return new Imagen
            {
                ImgGuid = Guid.NewGuid().ToString(),
                ImgDescripcion = model.Descripcion,
                ImgUrl = model.Url,
                AtId = model.AtraccionId,
                ImgFechaIngreso = DateTime.UtcNow,
                ImgIpIngreso = "127.0.0.1",
                ImgUsuarioIngreso = "system",
                ImgEstado = "ACT"
            };
        }
        public static void UpdateEntity(Imagen entity, ImagenUpdateModel model)
        {
            entity.AtId = model.AtraccionId;
            entity.ImgDescripcion = model.Descripcion;
            entity.ImgUrl = model.Url;
        }
    }
}
