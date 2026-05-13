using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("TICKET");

            // 🔑 PK
            builder.HasKey(e => e.TicId);

            // 🔗 Columnas
            builder.Property(e => e.TicId).HasColumnName("tck_id");
            builder.Property(e => e.TicGuid).HasColumnName("tck_guid");

            builder.Property(e => e.HorId).HasColumnName("hor_id");

            builder.Property(e => e.TicTitulo)
                   .HasColumnName("tck_titulo")
                   .HasMaxLength(150);

            builder.Property(e => e.TicPrecio)
                   .HasColumnName("tck_precio");

            builder.Property(e => e.TicTipoParticipante)
                   .HasColumnName("tck_tipo_participante")
                   .HasMaxLength(30);

            builder.Property(e => e.TicFechaIngreso)
                   .HasColumnName("tck_fecha_ingreso");

            builder.Property(e => e.TicUsuarioIngreso)
                   .HasColumnName("tck_usuario_ingreso")
                   .HasMaxLength(100);

            builder.Property(e => e.TicIpIngreso)
                   .HasColumnName("tck_ip_ingreso")
                   .HasMaxLength(45);

            builder.Property(e => e.TicFechaMod)
                   .HasColumnName("tck_fecha_mod");

            builder.Property(e => e.TicUsuarioMod)
                   .HasColumnName("tck_usuario_mod")
                   .HasMaxLength(100);

            builder.Property(e => e.TicIpMod)
                   .HasColumnName("tck_ip_mod")
                   .HasMaxLength(45);

            builder.Property(e => e.TicFechaEliminacion)
                   .HasColumnName("tck_fecha_eliminacion");

            builder.Property(e => e.TicUsuarioEliminacion)
                   .HasColumnName("tck_usuario_eliminacion")
                   .HasMaxLength(100);

            builder.Property(e => e.TicIpEliminacion)
                   .HasColumnName("tck_ip_eliminacion")
                   .HasMaxLength(45);

            builder.Property(e => e.TicEstado)
                   .HasColumnName("tck_estado")
                   .HasMaxLength(3);

            // 🔗 Relación con Horario
            builder.HasOne(e => e.Horario)
                   .WithMany(a => a.Ticket)
                   .HasForeignKey(e => e.HorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔗 Relación con DetalleReserva
            /*
            builder.HasMany(e => e.DetalleReserva)
                   .WithOne(d => d.Ticket)
                   .HasForeignKey(d => d.TicId)
                   .OnDelete(DeleteBehavior.Restrict);
            */
        }
    }
}
