using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Mappers;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Destino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Services
{
    public class DestinoDataService : IDestinoDataService
    {
        private readonly IDestinoQuery _query;
        private readonly IUnitOfWork _uow;

        public DestinoDataService(IDestinoQuery query, IUnitOfWork uow)
        {
            _query = query;
            _uow = uow;
        }

        public async Task<List<DestinoModel>> GetAllAsync()
        {
            var data = await _query.GetAllAsync();
            return data.Select(CatalogosMapper.ToModel).ToList();
        }

        public async Task<DestinoModel> GetByIdAsync(int id)
        {
            var data = await _query.GetByIdAsync(id);
            return CatalogosMapper.ToModel(data);
        }

        public async Task<int> CreateAsync(DestinoCreateModel model)
        {
            var entity = new Destino
            {
                DesGuid = Guid.NewGuid().ToString(),
                DesNombre = model.Nombre,
                DesPais = model.Pais,
                DesImagenUrl = model.ImagenUrl,
                DesEstado = "ACT",
                DesFechaIngreso = DateTime.UtcNow,
                DesUsuarioIngreso = "system",
                DesIpIngreso = "127.0.0.1"
            };

            await _uow.DestinoRepository.CreateAsync(entity);
            return entity.DesId;
        }

        public async Task UpdateAsync(DestinoUpdateModel model)
        {
            var entity = await _uow.DestinoRepository.GetByIdAsync(model.Id)
                ?? throw new Exception("Destino no encontrado");

            CatalogosMapper.UpdateEntity(entity, model);

            await _uow.DestinoRepository.UpdateAsync(entity);
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _uow.DestinoRepository.SoftDeleteAsync(id);
        }
    }
}
