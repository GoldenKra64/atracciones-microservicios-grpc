using Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Mappers;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Resena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Services
{
    public class ResenaDataService : IResenaDataService
    {
        private readonly IResenaQuery _query;
        private readonly IUnitOfWork _uow;
        private readonly IAtraccionQuery aquery;

        public ResenaDataService(IResenaQuery query, IUnitOfWork uow, IAtraccionQuery aquery)
        {
            _query = query;
            _uow = uow;
            this.aquery = aquery;
        }

        public async Task<List<ResenaModel>> GetByAtraccionAsync(string atraccionId)
        {
            var atId = await aquery.GetByIdAsync(atraccionId);
            var data = await _query.GetByAtraccionAsync(atId.AtId);
            return data.Select(ResenaMapper.ToModel).ToList();
        }

        public async Task<int> CreateAsync(ResenaCreateModel model)
        {
            var atraccion = await aquery.GetByIdAsync(model.AtraccionGuid);
            model.AtraccionId = atraccion.AtId;

            var entity = ResenaMapper.ToEntity(model);
            await _uow.ResenaRepository.CreateAsync(entity);
            return entity.ResenaId;
        }

        public async Task UpdateAsync(ResenaUpdateModel model)
        {
            var entity = await _uow.ResenaRepository.GetByIdAsync(model.Id)
                ?? throw new Exception("Reseña no encontrada");

            entity.ResenaCalificacion = model.Calificacion;
            entity.ResenaComentario = model.Comentario;

            await _uow.ResenaRepository.UpdateAsync(entity);
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _uow.ResenaRepository.SoftDeleteAsync(id);
        }
    }
}
