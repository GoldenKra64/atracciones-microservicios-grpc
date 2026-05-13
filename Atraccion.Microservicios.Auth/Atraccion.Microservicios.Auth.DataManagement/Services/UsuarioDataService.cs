using Atraccion.Microservicios.Auth.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Auth.DataManagement.Interfaces;
using Atraccion.Microservicios.Auth.DataManagement.Mappers;
using Atraccion.Microservicios.Auth.DataManagement.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataManagement.Services
{
    public class UsuarioDataService : IUsuarioDataService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUsuarioQuery _query;

        public UsuarioDataService(IUnitOfWork uow, IUsuarioQuery query)
        {
            _uow = uow;
            _query = query;
        }

        // ===============================
        // CREATE
        // ===============================
        public async Task<int> CreateAsync(UsuarioCreateModel model)
        {
            var entity = UsuarioMapper.ToEntity(model);

            await _uow.UsuarioRepository.CreateAsync(entity);

            return entity.UsuId;
        }

        // ===============================
        // UPDATE
        // ===============================
        public async Task UpdateAsync(UsuarioUpdateModel model)
        {
            var entity = await _uow.UsuarioRepository.GetByIdAsync(model.Id)
                ?? throw new Exception("Usuario no encontrado");

            UsuarioMapper.UpdateEntity(entity, model);

            await _uow.UsuarioRepository.UpdateAsync(entity);
        }

        // ===============================
        // GET BY ID
        // ===============================
        public async Task<UsuarioModel?> GetByIdAsync(int id)
        {
            var entity = await _uow.UsuarioRepository.GetByIdAsync(id);

            return entity == null
                ? null
                : UsuarioMapper.ToModel(entity);
        }

        public async Task<bool> UserIsRegistered(string login)
        {
            return await _query.UserIsAlreadyRegistered(login);
        }

        // ===============================
        // LOGIN
        // ===============================
        public async Task<UsuarioModel?> LoginAsync(string login, string password)
        {
            var entity = await _uow.UsuarioRepository.LoginAsync(login, password);

            return entity == null
                ? null
                : UsuarioMapper.ToModel(entity);
        }

        // ===============================
        // CHANGE PASSWORD
        // ===============================
        public async Task ChangePasswordAsync(int usuarioId, string actual, string nuevo)
        {
            var entity = await _uow.UsuarioRepository.GetByIdAsync(usuarioId)
                ?? throw new Exception("Usuario no encontrado");

            if (entity.UsuPasswordHash != actual)
                throw new Exception("Password actual incorrecto");

            entity.UsuPasswordHash = nuevo;

            await _uow.UsuarioRepository.UpdateAsync(entity);
        }

        // ===============================
        // SOFT DELETE
        // ===============================
        public async Task SoftDeleteAsync(int id)
        {
            var entity = await _uow.UsuarioRepository.GetByIdAsync(id)
                ?? throw new Exception("Usuario no encontrado");

            entity.UsuEstado = "INA";

            await _uow.UsuarioRepository.UpdateAsync(entity);
        }
    }
}
