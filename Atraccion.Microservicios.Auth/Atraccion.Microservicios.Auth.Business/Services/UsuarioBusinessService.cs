using Atraccion.Microservicios.Auth.Business.Common;
using Atraccion.Microservicios.Auth.Business.DTOs;
using Atraccion.Microservicios.Auth.Business.DTOs.Usuario;
using Atraccion.Microservicios.Auth.Business.Exceptions;
using Atraccion.Microservicios.Auth.Business.Interfaces;
using Atraccion.Microservicios.Auth.Business.Mappers;
using Atraccion.Microservicios.Auth.Business.Validators;
using Atraccion.Microservicios.Auth.DataManagement.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.Business.Services
{
    public class UsuarioBusinessService : IUsuarioBusinessService
    {
        private readonly IUsuarioDataService _dataService;
        private readonly IClienteIntegration _clienteIntegration;
        private readonly JwtSettings _jwtSettings;

        public UsuarioBusinessService(IUsuarioDataService dataService, IOptions<JwtSettings> jwtOptions, IClienteIntegration clienteIntegration)
        {
            _dataService = dataService;
            _jwtSettings = jwtOptions.Value;
            _clienteIntegration = clienteIntegration;
        }

        public async Task<UsuarioResponse> GetByIdAsync(int id)
        {
            var data = await _dataService.GetByIdAsync(id)
                ?? throw new NotFoundException("Usuario", id);

            return UsuarioBusinessMapper.ToResponse(data);
        }

        public async Task<int> CreateAsync(CreateUsuarioRequest request)
        {

            if (await _dataService.UserIsRegistered(request.Login))
            {
                throw new ValidationException("Usuario ya se encuentra registrado");
            }

            UsuarioValidator.ValidateCreate(request);
            ClienteValidator.ValidateCreate(request.Cliente);

            var model = UsuarioBusinessMapper.ToCreateModel(request);

            var id = await _dataService.CreateAsync(model);

            if (request.Cliente != null)
            {
                await _clienteIntegration.CreateClienteAsync(
                    id,
                    request.Cliente.TipoIdentificacion,
                    request.Cliente.NumeroIdentificacion,
                    request.Cliente.Correo, 
                    request.Cliente.Nombres, 
                    request.Cliente.Apellidos, 
                    request.Cliente.Telefono, 
                    request.Cliente.Direccion);
            }

            return id;
        }

        public async Task UpdateAsync(UpdateUsuarioRequest request)
        {
            var model = UsuarioBusinessMapper.ToUpdateModel(request);
            await _dataService.UpdateAsync(model);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var data = await _dataService.LoginAsync(request.Login, request.Password);

            Console.WriteLine($"Login attempt for user: {data}");
            if (data == null)
                throw new UnauthorizedBusinessException("Credenciales inválidas");

            var clienteId = await _clienteIntegration.GetByUsuarioAsync(data.Id);   // Arreglar

            var token = GenerateJwt.GenerateJwtToken(_jwtSettings, data.Login, data.Roles, clienteId);

            return new LoginResponse
            {
                Success = true,
                Message = "Login exitoso",
                Token = token.Token, //token.Token,
                Expiration = token.Expiration, //token.Expiration,
                Username = data.Login,
                Roles = data.Roles
            };
        }

        public async Task ChangePasswordAsync(ChangePasswordRequest request)
        {
            await _dataService.ChangePasswordAsync(
                request.UsuarioId,
                request.PasswordActual,
                request.PasswordNuevo);
        }

        public async Task LogicalDeleteAsync(int id)
        {
            await _dataService.SoftDeleteAsync(id);
        }
    }
}
