using Atraccion.Microservicios.Cliente.Api.Protos;
using Atraccion.Microservicios.Cliente.DataManagement.Interfaces;
using Atraccion.Microservicios.Cliente.DataManagement.Models.Cliente;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.Api.Grpc
{
    public class ClienteGrpcService : ClienteService.ClienteServiceBase
    {
        private readonly IClienteDataService _dataService;

        public ClienteGrpcService(IClienteDataService dataService)
        {
            _dataService = dataService;
        }

        public override async Task<ClienteResponse> GetClienteById(ClienteRequest request, ServerCallContext context)
        {
            try
            {
                var cliente = await _dataService.GetByIdAsync(request.CliId);
                if (cliente == null)
                    throw new RpcException(new Status(StatusCode.NotFound, $"Cliente con id {request.CliId} no encontrado."));

                return new ClienteResponse
                {
                    CliId = cliente.Id,
                    TipoIdentificacion = cliente.TipoIdentificacion,
                    NumeroIdentificacion = cliente.NumeroIdentificacion,
                    Correo = cliente.Correo,
                    Nombres = cliente.Nombres,
                    Apellidos = cliente.Apellidos,
                    Telefono = cliente.Telefono ?? string.Empty,
                    Direccion = cliente.Direccion ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<ClienteResponse> GetClienteByUsuario(ClienteByUsuarioRequest request, ServerCallContext context)
        {
            try
            {
                var cliente = await _dataService.GetByUsuarioAsync(request.UsuarioId);
                if (cliente == null)
                    throw new RpcException(new Status(StatusCode.NotFound, $"Cliente con usuario id {request.UsuarioId} no encontrado."));

                return new ClienteResponse
                {
                    CliId = cliente.Id,
                    NumeroIdentificacion = cliente.NumeroIdentificacion,
                    Correo = cliente.Correo,
                    Nombres = cliente.Nombres,
                    Apellidos = cliente.Apellidos,
                    Telefono = cliente.Telefono ?? string.Empty,
                    Direccion = cliente.Direccion ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<CreateClienteResponse> CreateCliente(CreateClienteRequest request, ServerCallContext context)
        {
            try
            {
                var id = await _dataService.CreateAsync(new ClienteCreateModel
                {
                    UsuarioId = request.UsuarioId,
                    TipoIdentificacion = request.TipoIdentificacion,
                    NumeroIdentificacion = request.NumeroIdentificacion,
                    Correo = request.Correo,
                    Nombres = request.Nombres,
                    Apellidos = request.Apellidos,
                    Telefono = request.Telefono,
                    Direccion = request.Direccion
                });

                return new CreateClienteResponse
                {
                    Success = true,
                    CliId = id
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
}
