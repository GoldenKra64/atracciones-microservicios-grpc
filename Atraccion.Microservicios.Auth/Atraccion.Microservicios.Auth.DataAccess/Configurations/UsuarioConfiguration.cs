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
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");

            builder.HasKey(x => x.UsuId);

            // 🔗 Columnas
            builder.Property(e => e.UsuId).HasColumnName("usu_id");
            builder.Property(e => e.UsuGuid).HasColumnName("usu_guid");

            builder.Property(e => e.UsuLogin)
                   .HasColumnName("usu_login")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.UsuPasswordHash)
                   .HasColumnName("usu_password_hash")
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(e => e.UsuFechaRegistro)
                   .HasColumnName("usu_fecha_registro");

            builder.Property(e => e.UsuUsuarioRegistro)
                   .HasColumnName("usu_usuario_registro")
                   .HasMaxLength(100);

            builder.Property(e => e.UsuIpRegistro)
                   .HasColumnName("usu_ip_registro")
                   .HasMaxLength(45);

            builder.Property(e => e.UsuFechaMod)
                   .HasColumnName("usu_fecha_mod");

            builder.Property(e => e.UsuUsuarioMod)
                   .HasColumnName("usu_usuario_mod")
                   .HasMaxLength(100);

            builder.Property(e => e.UsuIpMod)
                   .HasColumnName("usu_ip_mod")
                   .HasMaxLength(45);

            builder.Property(e => e.UsuFechaEliminacion)
                   .HasColumnName("usu_fecha_eliminacion");

            builder.Property(e => e.UsuUsuarioEliminacion)
                   .HasColumnName("usu_usuario_eliminacion")
                   .HasMaxLength(100);

            builder.Property(e => e.UsuIpEliminacion)
                   .HasColumnName("usu_ip_eliminacion")
                   .HasMaxLength(45);

            builder.Property(e => e.UsuEstado)
                   .HasColumnName("usu_estado")
                   .HasMaxLength(3);

            // 🔗 Relación con UsuarioRol
            builder.HasMany(e => e.UsuarioRoles)
                   .WithOne(ur => ur.Usuario)
                   .HasForeignKey(ur => ur.UsuId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
