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
    public class ImagenConfiguration : IEntityTypeConfiguration<Imagen>
    {
        public void Configure(EntityTypeBuilder<Imagen> builder)
        {
            builder.ToTable("IMAGEN");

            builder.HasKey(x => x.ImgId);

            builder.Property(e => e.ImgId).HasColumnName("img_id");
            builder.Property(e => e.ImgGuid).HasColumnName("img_guid");

            builder.Property(e => e.ImgUrl).HasColumnName("img_url");
            builder.Property(e => e.ImgDescripcion).HasColumnName("img_descripcion");

            builder.Property(e => e.AtId).HasColumnName("at_id");

            builder.Property(e => e.ImgFechaIngreso).HasColumnName("img_fecha_ingreso");
            builder.Property(e => e.ImgUsuarioIngreso).HasColumnName("img_usuario_ingreso");
            builder.Property(e => e.ImgIpIngreso).HasColumnName("img_ip_ingreso");

            builder.Property(e => e.ImgFechaMod).HasColumnName("img_fecha_mod");
            builder.Property(e => e.ImgUsuarioMod).HasColumnName("img_usuario_mod");
            builder.Property(e => e.ImgIpMod).HasColumnName("img_ip_mod");

            builder.Property(e => e.ImgFechaEliminacion).HasColumnName("img_fecha_eliminacion");
            builder.Property(e => e.ImgUsuarioEliminacion).HasColumnName("img_usuario_eliminacion");
            builder.Property(e => e.ImgIpEliminacion).HasColumnName("img_ip_eliminacion");

            builder.Property(e => e.ImgEstado).HasColumnName("img_estado");

            builder.Property(x => x.ImgUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.ImgDescripcion)
                .HasMaxLength(300);

            builder.Property(x => x.ImgEstado)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(x => x.Atraccion)
                .WithMany(a => a.Imagenes)
                .HasForeignKey(x => x.AtId);
        }
    }
}
