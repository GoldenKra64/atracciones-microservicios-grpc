using Atraccion.Microservicios.Auth.Business.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.Business.Interfaces
{
    public interface IUsuarioBusinessService
    {
        Task<UsuarioResponse> GetByIdAsync(int id);

        Task<int> CreateAsync(CreateUsuarioRequest request);

        Task UpdateAsync(UpdateUsuarioRequest request);

        Task<LoginResponse> LoginAsync(LoginRequest request);

        Task ChangePasswordAsync(ChangePasswordRequest request);

        Task LogicalDeleteAsync(int id);
    }
}
