using Atraccion.Microservicios.Reserva.DataAccess.Context;
using Atraccion.Microservicios.Reserva.DataAccess.Repositories.Interfaces;
using Atraccion.Microservicios.Reserva.DataManagement.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AtraccionesDbContext _context;
        private IDbContextTransaction? _transaction;
        public IReservaRepository ReservaRepository { get; }

        public UnitOfWork(
            AtraccionesDbContext context,
            IReservaRepository reservaRepository
        )
        {
            _context = context;
            ReservaRepository = reservaRepository;
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
