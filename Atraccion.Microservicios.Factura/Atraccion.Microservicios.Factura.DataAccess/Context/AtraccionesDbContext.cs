using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.DataAccess.Context
{
    public class AtraccionesDbContext : DbContext
    {
        public AtraccionesDbContext(DbContextOptions<AtraccionesDbContext> options)
            : base(options)
        {
        }

        #region DbSets
        public DbSet<Factura.DataAccess.Entities.Factura> Facturas => Set<Factura.DataAccess.Entities.Factura>();
        #endregion

        // 🔥 Aplicar configuraciones automáticamente
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AtraccionesDbContext).Assembly);

            // 🔥 Soft Delete Global Filter
            ApplySoftDeleteFilters(modelBuilder);
        }

        #region Soft Delete

        private void ApplySoftDeleteFilters(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var property = entityType.FindProperty("FechaEliminacion")
                            ?? entityType.FindProperty($"{entityType.ClrType.Name.Substring(0, 3)}FechaEliminacion");

                if (property != null)
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var propertyAccess = Expression.Property(parameter, property.Name);
                    var nullConstant = Expression.Constant(null);

                    var body = Expression.Equal(propertyAccess, nullConstant);
                    var lambda = Expression.Lambda(body, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

        #endregion

        #region Auditoria automática

        public override int SaveChanges()
        {
            ApplyAudit();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAudit();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAudit()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    SetValueIfExists(entry, "FechaIngreso", DateTime.UtcNow);
                }

                if (entry.State == EntityState.Modified)
                {
                    SetValueIfExists(entry, "FechaMod", DateTime.UtcNow);
                }

                if (entry.State == EntityState.Deleted)
                {
                    var prop = entry.Metadata.GetProperties()
                        .FirstOrDefault(p => p.Name == "FechaEliminacion" || p.Name.EndsWith("FechaEliminacion"));

                    if (prop != null)
                    {
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[prop.Name] = DateTime.UtcNow;
                    }
                }
            }
        }

        private void SetValueIfExists(EntityEntry entry, string propertyName, object value)
        {
            var prop = entry.Metadata.FindProperty(propertyName);

            if (prop != null)
            {
                entry.CurrentValues[propertyName] = value;
            }
        }

        #endregion
    }
}
