using Atraccion.Microservicios.Auth.DataManagement.Interfaces;
using Atraccion.Microservicios.Auth.DataManagement.Protos;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataManagement.Integrations
{
    public class ClienteIntegrationGrpc : IClienteIntegration
    {
        private readonly ClienteService.ClienteServiceClient _client;

        public ClienteIntegrationGrpc(ClienteService.ClienteServiceClient client)
        {
            _client = client;
        }

        public async Task<int> GetByUsuarioAsync(int usuarioId)
        {
            var response = await _client.GetClienteByUsuarioAsync(new ClienteByUsuarioRequest { UsuarioId = usuarioId });
            return response.CliId;
        }

        public async Task<int> CreateClienteAsync(int usuarioId, string tipoIdentificacion, string numeroIdentificacion, string correo, string nombres, string apellidos, string telefono, string direccion)
        {
            var request = new CreateClienteRequest
            {
                UsuarioId = usuarioId,
                TipoIdentificacion = tipoIdentificacion,
                NumeroIdentificacion = numeroIdentificacion,
                Correo = correo ?? string.Empty,
                Nombres = nombres ?? string.Empty,
                Apellidos = apellidos ?? string.Empty,
                Telefono = telefono ?? string.Empty,
                Direccion = direccion ?? string.Empty
            };

            var response = await _client.CreateClienteAsync(request);
            return response.CliId;
        }
    }
}
