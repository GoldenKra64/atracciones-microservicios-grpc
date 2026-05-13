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
    public class TagAtraccionConfiguration : IEntityTypeConfiguration<TagAtraccion>
    {
        public void Configure(EntityTypeBuilder<TagAtraccion> builder)
        {
            builder.ToTable("TAG_ATRACCION");

            builder.HasKey(x => new { x.TagId, x.AtId });

            builder.Property(e => e.TagId).HasColumnName("tag_id");
            builder.Property(e => e.AtId).HasColumnName("at_id");

            builder.HasOne(x => x.Tag)
                .WithMany(a => a.TagAtracciones)
                .HasForeignKey(x => x.TagId);

            builder.HasOne(x => x.Atraccion)
                .WithMany(a => a.TagAtracciones)
                .HasForeignKey(x => x.AtId);
        }
    }
}
