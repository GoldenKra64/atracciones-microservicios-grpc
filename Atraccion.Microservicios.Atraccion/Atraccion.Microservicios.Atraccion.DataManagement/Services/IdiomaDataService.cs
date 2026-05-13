using Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Mappers;
using Atraccion.Microservicios.Atraccion.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Services
{
    public class IdiomaDataService : IIdiomaDataService
    {
        private readonly IIdiomaQuery _query;

        public IdiomaDataService(IIdiomaQuery query)
        {
            _query = query;
        }

        public async Task<List<IdiomaModel>> GetAllAsync()
        {
            var data = await _query.GetAllAsync();
            return data.Select(CatalogosMapper.ToModel).ToList();
        }
    }
}
