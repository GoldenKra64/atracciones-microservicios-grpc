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
    public class NoIncluyeAtraccionConfiguration : IEntityTypeConfiguration<NoIncluyeAtraccion>
    {
        public void Configure(EntityTypeBuilder<NoIncluyeAtraccion> builder)
        {
            builder.ToTable("ATRACCION_NOINCLUYE");

            builder.HasKey(x => new { x.NoIncId, x.AtId });

            builder.Property(e => e.NoIncId).HasColumnName("noinc_id");
            builder.Property(e => e.AtId).HasColumnName("at_id");

            builder.HasOne(x => x.NoIncluye)
                .WithMany(i => i.NoIncluyeAtracciones)
                .HasForeignKey(x => x.NoIncId);

            builder.HasOne(x => x.Atraccion)
                .WithMany()
                .HasForeignKey(x => x.AtId);
        }
    }
}
