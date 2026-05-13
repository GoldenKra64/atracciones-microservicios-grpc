using Atraccion.Microservicios.Cliente.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Cliente.DataAccess.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente.DataAccess.Entities.Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente.DataAccess.Entities.Cliente> builder)
        {
            builder.ToTable("CLIENTES");

            builder.HasKey(x => x.CliId);

            builder.Property(e => e.CliId).HasColumnName("cli_id");
            builder.Property(e => e.CliGuid).HasColumnName("cli_guid");

            builder.Property(e => e.UsuId).HasColumnName("usu_id");

            builder.Property(e => e.CliTipoIdentificacion).HasColumnName("cli_tipo_identificacion");
            builder.Property(e => e.CliNumeroIdentificacion).HasColumnName("cli_numero_identificacion");

            builder.Property(e => e.CliNombres).HasColumnName("cli_nombres");
            builder.Property(e => e.CliApellidos).HasColumnName("cli_apellidos");

            builder.Property(e => e.CliCorreo).HasColumnName("cli_correo");
            builder.Property(e => e.CliTelefono).HasColumnName("cli_telefono");
            builder.Property(e => e.CliDireccion).HasColumnName("cli_direccion");

            builder.Property(e => e.CliFechaIngreso).HasColumnName("cli_fecha_ingreso");
            builder.Property(e => e.CliUsuarioIngreso).HasColumnName("cli_usuario_ingreso");
            builder.Property(e => e.CliIpIngreso).HasColumnName("cli_ip_ingreso");

            builder.Property(e => e.CliFechaEliminacion).HasColumnName("cli_fecha_eliminacion");
            builder.Property(e => e.CliUsuarioEliminacion).HasColumnName("cli_usuario_eliminacion");
            builder.Property(e => e.CliIpEliminacion).HasColumnName("cli_ip_eliminacion");

            builder.Property(e => e.CliEstado).HasColumnName("cli_estado");


            builder.Property(x => x.CliTipoIdentificacion)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.CliNumeroIdentificacion)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.CliCorreo)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.CliTelefono)
                .HasMaxLength(20);

            builder.Property(x => x.CliDireccion)
                .HasMaxLength(300);

            builder.Property(x => x.CliEstado)
                .IsRequired()
                .HasMaxLength(3);

            // 🔥 Relación Usuario
            /*
            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.Clientes)
                .HasForeignKey(x => x.UsuId)
                .OnDelete(DeleteBehavior.Restrict);
            */
        }
    }
}
