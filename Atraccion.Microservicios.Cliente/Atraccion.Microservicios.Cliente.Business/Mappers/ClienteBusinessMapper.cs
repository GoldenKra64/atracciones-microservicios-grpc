using Atraccion.Microservicios.Cliente.Business.DTOs.Cliente;
using Atraccion.Microservicios.Cliente.DataManagement.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.Business.Mappers
{
    public static class ClienteBusinessMapper
    {
        public static ClienteCreateModel ToCreateModel(CreateClienteRequest request)
        {
            return new ClienteCreateModel
            {
                UsuarioId = request.UsuarioId,
                TipoIdentificacion = request.TipoIdentificacion,
                NumeroIdentificacion = request.NumeroIdentificacion,
                Correo = request.Correo,
                Nombres = request.Nombres,
                Apellidos = request.Apellidos ?? string.Empty,
                Telefono = request.Telefono ?? string.Empty,
                Direccion = request.Direccion ?? string.Empty
            };
        }

        public static ClienteUpdateModel ToUpdateModel(UpdateClienteRequest request)
        {
            return new ClienteUpdateModel
            {
                Id = request.Id,
                TipoIdentificacion = request.TipoIdentificacion,
                NumeroIdentificacion = request.NumeroIdentificacion,
                Correo = request.Correo,
                Nombres = request.Nombres,
                Apellidos = request.Apellidos ?? string.Empty,
                Telefono = request.Telefono ?? string.Empty,
                Direccion = request.Direccion ?? string.Empty
            };
        }

        public static ClienteResponse ToResponse(ClienteModel model)
        {
            return new ClienteResponse
            {
                Id = model.Id,
                Guid = model.Guid,
                NumeroIdentificacion = model.NumeroIdentificacion,
                Correo = model.Correo,
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                Telefono = model.Telefono,
                Direccion = model.Direccion
            };
        }
    }
}
