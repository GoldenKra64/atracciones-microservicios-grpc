using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Atraccion.Microservicios.Atraccion.DataAccess.Queries.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Mappers;
using Atraccion.Microservicios.Atraccion.DataManagement.Models.Horario;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Services
{
    public class HorarioDataService : IHorarioDataService
    {
        private readonly IHorarioQuery _query;
        private readonly IUnitOfWork _uow;

        public HorarioDataService(IHorarioQuery query, IUnitOfWork uow)
        {
            _query = query;
            _uow = uow;
        }

        public async Task<List<HorarioModel>> GetAllAsync()
        {
            var data = await _query.GetAllAsync();
            return data.Select(HorarioMapper.ToModel).ToList();
        }

        public async Task<HorarioModel> GetByIdAsync(string id)
        {
            var data = await _query.GetByGuidAsync(id);
            return HorarioMapper.ToModel(data);
        }

        public async Task<int> CreateAsync(HorarioCreateModel model)
        {
            var entity = new Horario
            {
                HorGuid = Guid.NewGuid().ToString(),
                AtId = model.AtraccionId,
                HorFecha = DateTime.ParseExact(model.Fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                HorHoraInicio = TimeSpan.ParseExact(model.HoraInicio, "hh\\:mm", CultureInfo.InvariantCulture),
                HorHoraFin = model.HoraFin != null
                    ? TimeSpan.ParseExact(model.HoraFin, "hh\\:mm", CultureInfo.InvariantCulture)
                    : (TimeSpan?)null,
                HorCuposDisponibles = model.Cupos,
                HorEstado = "ACT",
                HorFechaIngreso = DateTime.UtcNow,
                HorUsuarioIngreso = "system",
                HorIpIngreso = "127.0.0.1"
            };

            await _uow.HorarioRepository.CreateAsync(entity);
            return entity.HorId;
        }

        public async Task UpdateAsync(HorarioUpdateModel model)
        {
            var entity = await _uow.HorarioRepository.GetByIdAsync(model.Id)
                ?? throw new Exception("Horario no encontrado");

            HorarioMapper.UpdateEntity(entity, model);

            await _uow.HorarioRepository.UpdateAsync(entity);
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _uow.HorarioRepository.SoftDeleteAsync(id);
        }
    }
}
