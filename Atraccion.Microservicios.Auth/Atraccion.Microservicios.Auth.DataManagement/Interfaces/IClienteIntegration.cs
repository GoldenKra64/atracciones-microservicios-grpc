using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataManagement.Interfaces
{
    public interface IClienteIntegration
    {
        Task<int> GetByUsuarioAsync(int usuarioId);
        Task<int> CreateClienteAsync(int usuarioId, string tipoIdentificacion, string numeroIdentificacion, string correo, string nombres, string apellidos, string telefono, string direccion);
    }
}
