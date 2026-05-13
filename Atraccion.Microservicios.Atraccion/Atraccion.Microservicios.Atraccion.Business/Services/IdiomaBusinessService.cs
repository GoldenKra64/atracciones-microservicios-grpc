using Atraccion.Microservicios.Atraccion.Business.DTOs.Idioma;
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
    public class IdiomaBusinessService : IIdiomaBusinessService
    {
        private readonly IIdiomaDataService _dataService;

        public IdiomaBusinessService(IIdiomaDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<IdiomaResponse>> GetAllAsync()
        {
            var data = await _dataService.GetAllAsync();
            return data.Select(CatalogosBusinessMapper.ToResponse);
        }
    }
}
