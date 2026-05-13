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
    public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
    {
        public void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            builder.ToTable("USUARIOXROL");

            // 🔑 PK
            builder.HasKey(e => e.UsuRolId);

            builder.Property(e => e.UsuRolId).HasColumnName("usu_rol_id");

            builder.Property(e => e.UsuId).HasColumnName("usu_id");
            builder.Property(e => e.RolId).HasColumnName("rol_id");

            // 🔗 Relación con Usuario
            builder.HasOne(e => e.Usuario)
                   .WithMany(u => u.UsuarioRoles)
                   .HasForeignKey(e => e.UsuId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔗 Relación con Rol
            builder.HasOne(e => e.Rol)
                   .WithMany(r => r.UsuarioRoles)
                   .HasForeignKey(e => e.RolId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
