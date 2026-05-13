using Atraccion.Microservicios.Atraccion.Business.DTOs.Imagen;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Atraccion.Microservicios.Atraccion.Business.Mappers;
using Atraccion.Microservicios.Atraccion.Business.Validators;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Services
{
    public class ImagenBusinessService : IImagenBusinessService
    {
        private readonly IImagenDataService _dataService;

        public ImagenBusinessService(IImagenDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<int> CreateAsync(CreateImagenRequest request)
        {
            ImagenValidator.ValidateCreate(request);
            var model = ImagenBusinessMapper.ToCreateModel(request);
            return await _dataService.CreateAsync(model);
        }

        public async Task<List<ImagenResponse>> GetAllAsync()
        {
            var model = await _dataService.GetAllAsync();

            return model.Select(ImagenBusinessMapper.ToResponse).ToList();
        }

        public async Task<ImagenResponse> GetByIdAsync(int id)
        {
            var model = await _dataService.GetByIdAsync(id);

            return ImagenBusinessMapper.ToResponse(model);
        }

        public async Task LogicalDeleteAsync(int id)
        {
            await _dataService.SoftDeleteAsync(id);
        }

        public async Task UpdateAsync(UpdateImagenRequest request)
        {
            ImagenValidator.ValidateUpdate(request);
            var model = ImagenBusinessMapper.ToUpdateModel(request);
            await _dataService.UpdateAsync(model);
        }
    }
}
