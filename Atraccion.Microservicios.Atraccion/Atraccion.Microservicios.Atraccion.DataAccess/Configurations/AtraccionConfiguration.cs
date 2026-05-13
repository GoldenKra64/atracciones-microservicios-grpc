using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Atraccion.Microservicios.Atraccion.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataAccess.Configurations
{
    public class AtraccionConfiguration : IEntityTypeConfiguration<Atraccion.DataAccess.Entities.Atraccion>
    {
        public void Configure(EntityTypeBuilder<Atraccion.DataAccess.Entities.Atraccion> builder)
        {
            builder.ToTable("ATRACCION");

            builder.HasKey(x => x.AtId);

            builder.Property(x => x.AtNombre)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.AtDescripcion)
                .HasMaxLength(2000);

            builder.Property(x => x.AtDireccion)
                .HasMaxLength(300);

            builder.Property(x => x.AtPuntoEncuentro)
                .HasMaxLength(300);

            builder.Property(x => x.AtPrecioReferencia)
                .HasColumnType("double(10,2)");

            builder.Property(x => x.AtEstado)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(e => e.AtId).HasColumnName("at_id");
            builder.Property(e => e.AtGuid).HasColumnName("at_guid");
            builder.Property(e => e.AtNombre).HasColumnName("at_nombre");
            builder.Property(e => e.AtNumEstablecimiento).HasColumnName("at_num_establecimiento");
            builder.Property(e => e.AtIncluyeTransporte).HasColumnName("at_incluye_transporte");
            builder.Property(e => e.AtDescripcion).HasColumnName("at_descripcion");
            builder.Property(e => e.AtDireccion).HasColumnName("at_direccion");
            builder.Property(e => e.AtPuntoEncuentro).HasColumnName("at_punto_encuentro");
            builder.Property(e => e.AtDuracionMinutos).HasColumnName("at_duracion_minutos");
            builder.Property(e => e.AtPrecioReferencia).HasColumnName("at_precio_referencia");
            builder.Property(e => e.AtIncluyeAcompaniante).HasColumnName("at_incluye_acompaniante");
            builder.Property(e => e.AtIncluyeTransporte).HasColumnName("at_incluye_transporte");

            builder.Property(e => e.DesId).HasColumnName("des_id");

            builder.Property(e => e.AtFechaIngreso).HasColumnName("at_fecha_ingreso");
            builder.Property(e => e.AtUsuarioIngreso).HasColumnName("at_usuario_ingreso");
            builder.Property(e => e.AtIpIngreso).HasColumnName("at_ip_ingreso");

            builder.Property(e => e.AtFechaMod).HasColumnName("at_fecha_mod");
            builder.Property(e => e.AtUsuarioMod).HasColumnName("at_usuario_mod");
            builder.Property(e => e.AtIpMod).HasColumnName("at_ip_mod");

            builder.Property(e => e.AtFechaEliminacion).HasColumnName("at_fecha_eliminacion");
            builder.Property(e => e.AtUsuarioEliminacion).HasColumnName("at_usuario_eliminacion");
            builder.Property(e => e.AtIpEliminacion).HasColumnName("at_ip_eliminacion");

            builder.Property(e => e.AtEstado).HasColumnName("at_estado");

            // 🔥 Relación con Destino
            builder.HasOne(x => x.Destino)
                .WithMany(d => d.Atracciones)
                .HasForeignKey(x => x.DesId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔥 Relación con Imagen
            builder.HasMany(x => x.Imagenes)
                .WithOne(i => i.Atraccion)
                .HasForeignKey(i => i.AtId);

            // 🔥 Relación con IncluyeAtracciones (Categoría-Atracción)
            builder.HasMany(x => x.IncluyeAtracciones)
                .WithOne(ia => ia.Atraccion)
                .HasForeignKey(ia => ia.AtId);

            // 🔥 Relación con Idioma
            builder.HasMany(x => x.IdiomaAtracciones)
                .WithOne(ia => ia.Atraccion)
                .HasForeignKey(ia => ia.AtId);

            // 🔥 Relación con Idioma
            builder.HasMany(x => x.TagAtracciones)
                .WithOne(ta => ta.Atraccion)
                .HasForeignKey(ta => ta.AtId);

            // 🔥 Relación con Idioma
            builder.HasMany(x => x.Resena)
                .WithOne(ta => ta.Atraccion)
                .HasForeignKey(ta => ta.AtId);
        }
    }
}
