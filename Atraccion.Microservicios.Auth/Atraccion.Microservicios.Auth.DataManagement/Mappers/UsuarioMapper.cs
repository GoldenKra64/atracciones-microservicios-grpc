using Atraccion.Microservicios.Auth.DataAccess.Entities;
using Atraccion.Microservicios.Auth.DataManagement.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataManagement.Mappers
{
    public static class UsuarioMapper
    {
        public static UsuarioModel ToModel(Usuario entity)
        {
            return new UsuarioModel
            {
                Id = entity.UsuId,
                Guid = entity.UsuGuid,
                Estado = entity.UsuEstado,
                Login = entity.UsuLogin,
                Roles = entity.UsuarioRoles.Select(ur => ur.Rol.RolDescripcion).ToList(),
            };
        }
        public static Usuario ToEntity(UsuarioCreateModel entity)
        {
            return new Usuario
            {
                UsuGuid = Guid.NewGuid().ToString(),
                UsuLogin = entity.Login,
                UsuPasswordHash = entity.Password,
                UsuarioRoles = entity.RolIds.Select(rolId => new UsuarioRol { RolId = rolId }).ToList(),
                UsuFechaRegistro = DateTime.UtcNow,
                UsuUsuarioRegistro = "SYSTEM",
                UsuIpRegistro = "127.0.0.1",
                UsuEstado = "ACT"
            };
        }
        public static void UpdateEntity(Usuario entity, UsuarioUpdateModel model)
        {
            entity.UsuLogin = model.Login;
            entity.UsuarioRoles = model.RolIds.Select(rolId => new UsuarioRol { RolId = rolId }).ToList();
        }
    }
}
