using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataAccess.Configurations
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva.DataAccess.Entities.Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva.DataAccess.Entities.Reserva> builder)
        {
            builder.ToTable("RESERVAS");

            // 🔑 PK
            builder.HasKey(e => e.RevId);

            // 🔗 Columnas
            builder.Property(e => e.RevId).HasColumnName("rev_id");
            builder.Property(e => e.RevGuid).HasColumnName("rev_guid");

            builder.Property(e => e.RevCodigo)
                   .HasColumnName("rev_codigo")
                   .HasMaxLength(20);

            builder.Property(e => e.CliId).HasColumnName("cli_id");

            builder.Property(e => e.RevFechaReservaUtc)
                   .HasColumnName("rev_fecha_reserva_utc");

            builder.Property(e => e.RevSubtotal)
                   .HasColumnName("rev_subtotal");

            builder.Property(e => e.RevValorIva)
                   .HasColumnName("rev_valor_iva");

            builder.Property(e => e.RevTotal)
                   .HasColumnName("rev_total");

            builder.Property(e => e.RevUsuarioIngreso)
                   .HasColumnName("rev_usuario_ingreso")
                   .HasMaxLength(100);

            builder.Property(e => e.RevIpIngreso)
                   .HasColumnName("rev_ip_ingreso")
                   .HasMaxLength(45);

            builder.Property(e => e.RevFechaMod)
                   .HasColumnName("rev_fecha_mod");

            builder.Property(e => e.RevUsuarioMod)
                   .HasColumnName("rev_usuario_mod")
                   .HasMaxLength(100);

            builder.Property(e => e.RevIpMod)
                   .HasColumnName("rev_ip_mod")
                   .HasMaxLength(45);

            builder.Property(e => e.RevFechaCancelacion)
                   .HasColumnName("rev_fecha_cancelacion");

            builder.Property(e => e.RevUsuarioCancelacion)
                   .HasColumnName("rev_usuario_cancelacion")
                   .HasMaxLength(100);

            builder.Property(e => e.RevIpCancelacion)
                   .HasColumnName("rev_ip_cancelacion")
                   .HasMaxLength(45);

            builder.Property(e => e.RevMotivoCancelacion)
                   .HasColumnName("rev_motivo_cancelacion")
                   .HasMaxLength(300);

            builder.Property(e => e.RevEstado)
                   .HasColumnName("rev_estado")
                   .HasMaxLength(3);

            builder.HasMany(x => x.Detalles)
                .WithOne(d => d.Reserva)
                .HasForeignKey(d => d.RevId);
        }
    }
}
