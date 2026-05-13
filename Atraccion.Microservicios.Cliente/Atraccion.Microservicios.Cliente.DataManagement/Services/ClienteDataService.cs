using Atraccion.Microservicios.Cliente.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Cliente.DataManagement.Interfaces;
using Atraccion.Microservicios.Cliente.DataManagement.Mappers;
using Atraccion.Microservicios.Cliente.DataManagement.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataManagement.Services
{
    public class ClienteDataService : IClienteDataService
    {
        private readonly IClienteQuery _query;
        private readonly IUnitOfWork _uow;

        public ClienteDataService(IClienteQuery query, IUnitOfWork uow)
        {
            _query = query;
            _uow = uow;
        }

        public async Task<ClienteModel?> GetByUsuarioAsync(int usuarioId)
        {
            var entity = await _query.GetByUsuarioAsync(usuarioId);
            return entity == null ? null : ClienteMapper.ToModel(entity);
        }

        public async Task<int> CreateAsync(ClienteCreateModel model)
        {
            var entity = ClienteMapper.ToEntity(model);
            await _uow.ClienteRepository.CreateAsync(entity);
            return entity.CliId;
        }

        public async Task UpdateAsync(ClienteUpdateModel model)
        {
            var entity = await _uow.ClienteRepository.GetByIdAsync(model.Id)
                ?? throw new Exception("Cliente no encontrado");

            ClienteMapper.UpdateEntity(entity, model);

            await _uow.ClienteRepository.UpdateAsync(entity);
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _uow.ClienteRepository.SoftDeleteAsync(id);
        }
        public async Task<ClienteModel?> GetByIdAsync(int id)
        {
            var entity = await _uow.ClienteRepository.GetByIdAsync(id);
            return entity == null ? null : ClienteMapper.ToModel(entity);
        }

        public async Task<IEnumerable<ClienteModel>> GetAllAsync()
        {
            var data = await _uow.ClienteRepository.GetAllAsync();
            return data.Select(ClienteMapper.ToModel);
        }
    }
}
