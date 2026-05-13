using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataAccess.Queries.Interfaces
{
    public interface IUsuarioQuery
    {
        Task<bool> UserIsAlreadyRegistered(string login);
    }
}
