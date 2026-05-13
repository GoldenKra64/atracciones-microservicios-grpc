using Atraccion.Microservicios.Auth.DataAccess.Context;
using Atraccion.Microservicios.Auth.DataAccess.Repositories.Interfaces;
using Atraccion.Microservicios.Auth.DataManagement.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataManagement.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AtraccionesDbContext _context;
        private IDbContextTransaction? _transaction;

        public IUsuarioRepository UsuarioRepository { get; }

        public UnitOfWork(
            AtraccionesDbContext context,
            IUsuarioRepository usuarioRepository
        )
        {
            _context = context;

            UsuarioRepository = usuarioRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
