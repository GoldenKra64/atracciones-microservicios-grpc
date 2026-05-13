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
    public class DestinoConfiguration : IEntityTypeConfiguration<Destino>
    {
        public void Configure(EntityTypeBuilder<Destino> builder)
        {
            builder.ToTable("DESTINO");

            builder.HasKey(x => x.DesId);

            builder.Property(e => e.DesId).HasColumnName("des_id");
            builder.Property(e => e.DesGuid).HasColumnName("des_guid");

            builder.Property(e => e.DesNombre).HasColumnName("des_nombre");
            builder.Property(e => e.DesPais).HasColumnName("des_pais");
            builder.Property(e => e.DesImagenUrl).HasColumnName("des_imagen_url");

            builder.Property(e => e.DesFechaIngreso).HasColumnName("des_fecha_ingreso");
            builder.Property(e => e.DesUsuarioIngreso).HasColumnName("des_usuario_ingreso");
            builder.Property(e => e.DesIpIngreso).HasColumnName("des_ip_ingreso");

            builder.Property(e => e.DesFechaMod).HasColumnName("des_fecha_mod");
            builder.Property(e => e.DesUsuarioMod).HasColumnName("des_usuario_mod");
            builder.Property(e => e.DesIpMod).HasColumnName("des_ip_mod");

            builder.Property(e => e.DesFechaEliminacion).HasColumnName("des_fecha_eliminacion");
            builder.Property(e => e.DesUsuarioEliminacion).HasColumnName("des_usuario_eliminacion");
            builder.Property(e => e.DesIpEliminacion).HasColumnName("des_ip_eliminacion");

            builder.Property(e => e.DesEstado).HasColumnName("des_estado");

            builder.Property(x => x.DesNombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.DesPais)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.DesImagenUrl)
                .HasMaxLength(500);

            builder.Property(x => x.DesEstado)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
