using Atraccion.Microservicios.Cliente.Business.DTOs.Cliente;
using Atraccion.Microservicios.Cliente.Business.Exceptions;
using Atraccion.Microservicios.Cliente.Business.Interfaces;
using Atraccion.Microservicios.Cliente.Business.Mappers;
using Atraccion.Microservicios.Cliente.Business.Validators;
using Atraccion.Microservicios.Cliente.DataManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.Business.Services
{
    public class ClienteBusinessService : IClienteBusinessService
    {
        private readonly IClienteDataService _dataService;

        public ClienteBusinessService(IClienteDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<int> CreateAsync(CreateClienteRequest request)
        {
            ClienteValidator.ValidateCreate(request);
            var model = ClienteBusinessMapper.ToCreateModel(request);
            return await _dataService.CreateAsync(model);
        }

        public async Task UpdateAsync(UpdateClienteRequest request)
        {
            ClienteValidator.ValidateUpdate(request);
            var model = ClienteBusinessMapper.ToUpdateModel(request);
            await _dataService.UpdateAsync(model);
        }

        public async Task<ClienteResponse> GetByIdAsync(int id)
        {
            var data = await _dataService.GetByIdAsync(id)
                ?? throw new NotFoundException("Cliente", id);

            return ClienteBusinessMapper.ToResponse(data);
        }

        public async Task<IEnumerable<ClienteResponse>> GetAllAsync()
        {
            var data = await _dataService.GetAllAsync();
            return data.Select(ClienteBusinessMapper.ToResponse);
        }

        public async Task LogicalDeleteAsync(int id)
        {
            await _dataService.SoftDeleteAsync(id);
        }
    }
}
