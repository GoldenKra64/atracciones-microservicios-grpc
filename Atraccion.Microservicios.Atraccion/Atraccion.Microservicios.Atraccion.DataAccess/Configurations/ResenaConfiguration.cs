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
    public class ResenaConfiguration : IEntityTypeConfiguration<Resena>
    {
        public void Configure(EntityTypeBuilder<Resena> builder)
        {
            builder.ToTable("RESENIA");

            // 🔑 PK
            builder.HasKey(e => e.ResenaId);

            // 🔗 Columnas
            builder.Property(e => e.ResenaId).HasColumnName("rsn_id");
            builder.Property(e => e.ResenaGuid).HasColumnName("rsn_guid");

            builder.Property(e => e.AtId).HasColumnName("at_id");
            builder.Property(e => e.RevId).HasColumnName("rev_id");
            builder.Property(e => e.CliId).HasColumnName("cli_id");

            builder.Property(e => e.ResenaComentario)
                   .HasColumnName("rsn_comentario")
                   .HasMaxLength(1000);

            builder.Property(e => e.ResenaCalificacion)
                   .HasColumnName("rsn_rating");

            builder.Property(e => e.ResenaFechaCreacion)
                   .HasColumnName("rsn_fecha_creacion");

            builder.Property(e => e.ResenaUsuarioCreacion)
                   .HasColumnName("rsn_usuario_creacion")
                   .HasMaxLength(100);

            builder.Property(e => e.ResenaIpCreacion)
                   .HasColumnName("rsn_ip_creacion")
                   .HasMaxLength(45);

            builder.Property(e => e.ResenaFechaMod)
                   .HasColumnName("rsn_fecha_mod");

            builder.Property(e => e.ResenaUsuarioMod)
                   .HasColumnName("rsn_usuario_mod")
                   .HasMaxLength(100);

            builder.Property(e => e.ResenaIpMod)
                   .HasColumnName("rsn_ip_mod")
                   .HasMaxLength(45);

            builder.Property(e => e.ResenaFechaEliminacion)
                   .HasColumnName("rsn_fecha_eliminacion");

            builder.Property(e => e.ResenaUsuarioEliminacion)
                   .HasColumnName("rsn_usuario_eliminacion")
                   .HasMaxLength(100);

            builder.Property(e => e.ResenaIpEliminacion)
                   .HasColumnName("rsn_ip_eliminacion")
                   .HasMaxLength(45);

            builder.Property(e => e.ResenaEstado)
                   .HasColumnName("rsn_estado")
                   .HasMaxLength(3);

            /*
            builder.HasOne(e => e.Reserva)
           .WithMany(r => r.Resenas)
           .HasForeignKey(e => e.RevId)
           .OnDelete(DeleteBehavior.Restrict);
            */

            builder.HasOne(e => e.Atraccion)
                .WithMany(a => a.Resena)
                .HasForeignKey(e => e.AtId)
                .OnDelete(DeleteBehavior.Restrict);

            /*
            builder.HasOne(e => e.Cliente)
                .WithMany(c => c.Resenas)
                .HasForeignKey(e => e.CliId)
                .OnDelete(DeleteBehavior.Restrict);
            */
        }
    }
}
