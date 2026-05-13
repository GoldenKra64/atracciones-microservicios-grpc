using global::Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Context
{

    public class AtraccionesDbContext : DbContext
    {
        public AtraccionesDbContext(DbContextOptions<AtraccionesDbContext> options)
            : base(options)
        {
        }

        #region DbSets

        public DbSet<Destino> Destinos => Set<Destino>();
        public DbSet<Atraccion.DataAccess.Entities.Atraccion> Atracciones => Set<Atraccion.DataAccess.Entities.Atraccion>();
        public DbSet<Imagen> Imagenes => Set<Imagen>();

        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<CategoriaAtraccion> CategoriaAtracciones => Set<CategoriaAtraccion>();

        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Horario> Horarios => Set<Horario>();

        public DbSet<Resena> Resenas => Set<Resena>();

        public DbSet<Incluye> Incluyes => Set<Incluye>();
        public DbSet<IncluyeAtraccion> IncluyeAtracciones => Set<IncluyeAtraccion>();

        public DbSet<NoIncluye> NoIncluyes => Set<NoIncluye>();
        public DbSet<NoIncluyeAtraccion> NoIncluyeAtracciones => Set<NoIncluyeAtraccion>();

        public DbSet<Idioma> Idiomas => Set<Idioma>();
        public DbSet<IdiomaAtraccion> IdiomaAtracciones => Set<IdiomaAtraccion>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<TagAtraccion> TagAtracciones => Set<TagAtraccion>();

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
