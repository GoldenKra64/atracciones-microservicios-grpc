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
    public class HorarioConfiguration : IEntityTypeConfiguration<Horario>
    {
        public void Configure(EntityTypeBuilder<Horario> builder)
        {
            builder.ToTable("HORARIO");

            // 🔑 PK
            builder.HasKey(e => e.HorId);

            // 🔗 Columnas
            builder.Property(e => e.HorId)
                   .HasColumnName("hor_id");

            builder.Property(e => e.HorGuid)
                   .HasColumnName("hor_guid")
                   .IsRequired();

            builder.Property(e => e.AtId)
                   .HasColumnName("at_id")
                   .IsRequired();

            builder.Property(e => e.HorFecha)
                   .HasColumnName("hor_fecha")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(e => e.HorHoraInicio)
                   .HasColumnName("hor_hora_inicio")
                   .HasColumnType("time")
                   .IsRequired();

            builder.Property(e => e.HorHoraFin)
                   .HasColumnName("hor_hora_fin")
                   .HasColumnType("time");

            builder.Property(e => e.HorCuposDisponibles)
                   .HasColumnName("hor_cupos_disponibles")
                   .IsRequired();

            // 🧾 Auditoría ingreso
            builder.Property(e => e.HorFechaIngreso)
                   .HasColumnName("hor_fecha_ingreso")
                   .IsRequired();

            builder.Property(e => e.HorUsuarioIngreso)
                   .HasColumnName("hor_usuario_ingreso")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.HorIpIngreso)
                   .HasColumnName("hor_ip_ingreso")
                   .HasMaxLength(45)
                   .IsRequired();

            // 🧾 Auditoría modificación
            builder.Property(e => e.HorFechaMod)
                   .HasColumnName("hor_fecha_mod");

            builder.Property(e => e.HorUsuarioMod)
                   .HasColumnName("hor_usuario_mod")
                   .HasMaxLength(100);

            builder.Property(e => e.HorIpMod)
                   .HasColumnName("hor_ip_mod")
                   .HasMaxLength(45);

            // 🧾 Eliminación lógica
            builder.Property(e => e.HorFechaEliminacion)
                   .HasColumnName("hor_fecha_eliminacion");

            builder.Property(e => e.HorUsuarioEliminacion)
                   .HasColumnName("hor_usuario_eliminacion")
                   .HasMaxLength(100);

            builder.Property(e => e.HorIpEliminacion)
                   .HasColumnName("hor_ip_eliminacion")
                   .HasMaxLength(45);

            builder.Property(e => e.HorEstado)
                   .HasColumnName("hor_estado")
                   .HasMaxLength(3)
                   .IsRequired();

            // 🔗 Índices (MUY IMPORTANTES)
            builder.HasIndex(e => e.HorGuid)
                   .IsUnique()
                   .HasDatabaseName("UK_HORARIO_guid");

            // 🔗 Relación con Ticket
            builder.HasMany(e => e.Ticket)
                   .WithOne(t => t.Horario)
                   .HasForeignKey(t => t.TicId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relacion con Atraccion
            builder.HasOne(e => e.Atraccion)
                   .WithMany(a => a.Horario)
                   .HasForeignKey(e => e.AtId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
