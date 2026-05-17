using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.DataAccess.Configurations
{
    public class FacturaConfiguration : IEntityTypeConfiguration<Factura.DataAccess.Entities.Factura>
    {
        public void Configure(EntityTypeBuilder<Factura.DataAccess.Entities.Factura> builder)
        {
            builder.ToTable("FACTURAS");

            // 🔑 PK
            builder.HasKey(e => e.FacId);

            // 🔗 Columnas
            builder.Property(e => e.FacId).HasColumnName("fac_id");
            builder.Property(e => e.FacGuid).HasColumnName("fac_guid");

            builder.Property(e => e.RevId).HasColumnName("rev_id");
            builder.Property(e => e.CliId).HasColumnName("cli_id");

            builder.Property(e => e.FacNumero).HasColumnName("fac_numero");
            builder.Property(e => e.FacFechaEmision).HasColumnName("fac_fecha_emision");

            builder.Property(e => e.FacTotal).HasColumnName("fac_total");

            builder.Property(e => e.FacObservacion).HasColumnName("fac_observacion");
            builder.Property(e => e.FacOrigenCanal).HasColumnName("fac_origen_canal");

            builder.Property(e => e.FacCorreoReceptor).HasColumnName("fac_correo_receptor");
            builder.Property(e => e.FacNombreReceptor).HasColumnName("fac_nombre_receptor");

            builder.Property(e => e.FacUsuarioIngreso).HasColumnName("fac_usuario_ingreso");
            builder.Property(e => e.FacIpIngreso).HasColumnName("fac_ip_ingreso");

            builder.Property(e => e.FacFechaMod).HasColumnName("fac_fecha_mod");
            builder.Property(e => e.FacUsuarioMod).HasColumnName("fac_usuario_mod");
            builder.Property(e => e.FacIpMod).HasColumnName("fac_ip_mod");

            builder.Property(e => e.FacFechaEliminacion).HasColumnName("fac_fecha_eliminacion");
            builder.Property(e => e.FacUsuarioEliminacion).HasColumnName("fac_usuario_eliminacion");
            builder.Property(e => e.FacIpEliminacion).HasColumnName("fac_ip_eliminacion");

            builder.Property(e => e.FacEstado).HasColumnName("fac_estado");

            builder.Property(x => x.FacNumero)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.FacObservacion)
                .HasMaxLength(500);

            builder.Property(x => x.FacOrigenCanal)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.FacEstado)
                .IsRequired()
                .HasMaxLength(20);

            // 🔥 RELACIONES
            /*
            builder.HasOne(x => x.Reserva)
                .WithOne(r => r.Factura)
                .HasForeignKey<Factura>(x => x.RevId);
            */
        }
    }
}
