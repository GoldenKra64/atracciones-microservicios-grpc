using Atraccion.Microservicios.Auth.Business.DTOs.Usuario;
using Atraccion.Microservicios.Auth.DataManagement.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.Business.Mappers
{
    public static class UsuarioBusinessMapper
    {
        public static UsuarioCreateModel ToCreateModel(CreateUsuarioRequest request)
        {
            return new UsuarioCreateModel
            {
                Login = request.Login,
                Password = request.Password,
                RolIds = request.RolIds
            };
        }

        public static UsuarioUpdateModel ToUpdateModel(UpdateUsuarioRequest request)
        {
            return new UsuarioUpdateModel
            {
                Id = request.Id,
                Login = request.Login,
                RolIds = request.RolIds
            };
        }

        public static UsuarioResponse ToResponse(UsuarioModel model)
        {
            return new UsuarioResponse
            {
                Login = model.Login,
                Roles = model.Roles
            };
        }
    }
}
