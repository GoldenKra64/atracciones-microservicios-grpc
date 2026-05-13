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
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("TAG");

            builder.HasKey(x => x.TagId);

            builder.Property(e => e.TagId).HasColumnName("tag_id");
            builder.Property(e => e.TagDescription).HasColumnName("tag_description");

            builder.HasMany(x => x.TagAtracciones)
                .WithOne(a => a.Tag)
                .HasForeignKey(x => x.TagId);
        }
    }
}
