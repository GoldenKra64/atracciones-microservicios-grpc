using Atraccion.Microservicios.Cliente.DataAccess.Common;
using Atraccion.Microservicios.Cliente.DataAccess.Context;
using Atraccion.Microservicios.Cliente.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AtraccionesDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AtraccionesDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task SoftDeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;

            var prop = entity.GetType().GetProperties().FirstOrDefault(p => p.Name.EndsWith("Estado"));
            if (prop != null)
            {
                StatusChange.SetEstado(entity, "INA");
            }

            await _context.SaveChangesAsync();
        }
    }
}
