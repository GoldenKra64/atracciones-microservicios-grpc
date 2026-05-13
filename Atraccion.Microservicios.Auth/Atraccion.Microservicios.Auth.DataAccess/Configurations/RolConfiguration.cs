using Atraccion.Microservicios.Auth.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Auth.DataAccess.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("ROL");

            builder.HasKey(x => x.RolId);

            // 🔗 Columnas
            builder.Property(e => e.RolId).HasColumnName("rol_id");
            builder.Property(e => e.RolGuid).HasColumnName("rol_guid");

            builder.Property(e => e.RolDescripcion)
                   .HasColumnName("rol_descripcion")
                   .HasMaxLength(80);

            builder.Property(e => e.RolEstado)
                   .HasColumnName("rol_estado")
                   .HasMaxLength(3);

            builder.HasMany(e => e.UsuarioRoles)
                   .WithOne(ur => ur.Rol)
                   .HasForeignKey(ur => ur.RolId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
