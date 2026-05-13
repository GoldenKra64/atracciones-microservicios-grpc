using Atraccion.Microservicios.Cliente.DataManagement.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataManagement.Mappers
{
    public static class ClienteMapper
    {
        public static ClienteModel ToModel(Cliente.DataAccess.Entities.Cliente entity)
        {
            return new ClienteModel
            {
                Id = entity.CliId,
                Guid = entity.CliGuid,
                Estado = entity.CliEstado,

                TipoIdentificacion = entity.CliTipoIdentificacion,
                NumeroIdentificacion = entity.CliNumeroIdentificacion,
                Correo = entity.CliCorreo,
                Nombres = entity.CliNombres,
                Apellidos = entity.CliApellidos,
                Telefono = entity.CliTelefono,
                Direccion = entity.CliDireccion
            };
        }

        public static Cliente.DataAccess.Entities.Cliente ToEntity(ClienteCreateModel model)
        {
            return new Cliente.DataAccess.Entities.Cliente
            {
                UsuId = model.UsuarioId,
                CliGuid = Guid.NewGuid().ToString(),
                CliTipoIdentificacion = model.TipoIdentificacion,
                CliNumeroIdentificacion = model.NumeroIdentificacion,
                CliCorreo = model.Correo,
                CliNombres = model.Nombres,
                CliApellidos = model.Apellidos,
                CliTelefono = model.Telefono,
                CliDireccion = model.Direccion,
                CliFechaIngreso = DateTime.UtcNow,
                CliUsuarioIngreso = "System",
                CliIpIngreso = "127.0.0.1",
                CliEstado = "ACT"
            };
        }
        public static void UpdateEntity(Cliente.DataAccess.Entities.Cliente entity, ClienteUpdateModel model)
        {
            entity.CliTipoIdentificacion = model.TipoIdentificacion;
            entity.CliNumeroIdentificacion = model.NumeroIdentificacion;
            entity.CliCorreo = model.Correo;
            entity.CliNombres = model.Nombres;
            entity.CliApellidos = model.Apellidos;
            entity.CliDireccion = model.Direccion;
            entity.CliTelefono = model.Telefono;
        }
    }
}
