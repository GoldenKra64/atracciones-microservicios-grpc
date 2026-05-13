using Atraccion.Microservicios.Auth.DataManagement.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataManagement.Interfaces
{
    public interface IUsuarioDataService
    {
        Task<int> CreateAsync(UsuarioCreateModel model);

        Task UpdateAsync(UsuarioUpdateModel model);

        Task<UsuarioModel?> GetByIdAsync(int id);

        Task<UsuarioModel?> LoginAsync(string login, string password);
        Task<bool> UserIsRegistered(string login);

        Task ChangePasswordAsync(int usuarioId, string actual, string nuevo);

        Task SoftDeleteAsync(int id);
    }
}
