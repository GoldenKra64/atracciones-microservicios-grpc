using Atraccion.Microservicios.Atraccion.Business.DTOs.Categoria;
using Atraccion.Microservicios.Atraccion.Business.Interfaces;
using Atraccion.Microservicios.Atraccion.Business.Mappers;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Services
{
    public class CategoriaBusinessService : ICategoriaBusinessService
    {
        private readonly ICategoriaDataService _dataService;

        public CategoriaBusinessService(ICategoriaDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<CategoriaResponse>> GetAllAsync()
        {
            var data = await _dataService.GetTreeAsync();
            return data.Select(CatalogosBusinessMapper.ToResponse);
        }
    }
}
