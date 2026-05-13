using Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Mappers;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Imagen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Services
{
    public class ImagenDataService : IImagenDataService
    {
        private readonly IUnitOfWork _uow;
        private readonly IImagenQuery _query;

        public ImagenDataService(IUnitOfWork uow, IImagenQuery query)
        {
            _uow = uow;
            _query = query;
        }

        public async Task<int> CreateAsync(ImagenCreateModel model)
        {
            var entity = ImagenMapper.ToEntity(model);

            await _uow.ImagenRepository.CreateAsync(entity);
            return entity.ImgId;
        }

        public async Task<List<ImagenModel>> GetAllAsync()
        {
            var model = await _query.GetAllAsync();
            return model.Select(ImagenMapper.ToModel).ToList();
        }

        public async Task<ImagenModel> GetByIdAsync(int id)
        {
            var model = await _uow.ImagenRepository.GetByIdAsync(id);
            return ImagenMapper.ToModel(model);
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _uow.ImagenRepository.SoftDeleteAsync(id);
        }

        public async Task UpdateAsync(ImagenUpdateModel model)
        {
            var entity = await _uow.ImagenRepository.GetByIdAsync(model.Id)
                 ?? throw new Exception("Imagen no encontrada");

            ImagenMapper.UpdateEntity(entity, model);

            await _uow.ImagenRepository.UpdateAsync(entity);
        }
    }
}
