using Atraccion.Microservicios.Atraccion.DataAccess.Context;
using Atraccion.Microservicios.Atraccion.DataAccess.Repositories.Interfaces;
using Atraccion.Microservicios.Atraccion.DataManagement.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AtraccionesDbContext _context;
        private IDbContextTransaction? _transaction;

        public ITicketRepository TicketRepository { get; }
        public IAtraccionRepository AtraccionRepository { get; }
        public IImagenRepository ImagenRepository { get; }
        public ICategoriaRepository CategoriaRepository { get; }
        public IDestinoRepository DestinoRepository { get; }
        public IIncluyeRepository IncluyeRepository { get; }
        public IResenaRepository ResenaRepository { get; }
        public IHorarioRepository HorarioRepository { get; }

        public UnitOfWork(
            AtraccionesDbContext context,
            ITicketRepository ticketRepository,
            IAtraccionRepository atraccionRepository,
            IImagenRepository imagenRepository,
            ICategoriaRepository categoriaRepository,
            IDestinoRepository destinoRepository,
            IIncluyeRepository incluyeRepository,
            IResenaRepository resenaRepository,
            IHorarioRepository horarioRepository
        )
        {
            _context = context;
            TicketRepository = ticketRepository;
            AtraccionRepository = atraccionRepository;
            ImagenRepository = imagenRepository;
            CategoriaRepository = categoriaRepository;
            DestinoRepository = destinoRepository;
            IncluyeRepository = incluyeRepository;
            ResenaRepository = resenaRepository;
            HorarioRepository = horarioRepository;
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
