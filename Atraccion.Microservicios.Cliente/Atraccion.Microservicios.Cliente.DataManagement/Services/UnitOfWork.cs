using Atraccion.Microservicios.Cliente.DataAccess.Context;
using Atraccion.Microservicios.Cliente.DataAccess.Repositories.Interfaces;
using Atraccion.Microservicios.Cliente.DataManagement.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataManagement.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AtraccionesDbContext _context;
        private IDbContextTransaction? _transaction;
        public IClienteRepository ClienteRepository { get; }

        public UnitOfWork(
            AtraccionesDbContext context,
            IClienteRepository clienteRepository
        )
        {
            _context = context;

            ClienteRepository = clienteRepository;
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
