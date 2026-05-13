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
    public class IdiomaAtraccionConfiguration : IEntityTypeConfiguration<IdiomaAtraccion>
    {
        public void Configure(EntityTypeBuilder<IdiomaAtraccion> builder)
        {
            builder.ToTable("IDIOMA_ATRACCION");

            builder.HasKey(x => new { x.IdId, x.AtId });

            builder.Property(e => e.IdId).HasColumnName("id_id");
            builder.Property(e => e.AtId).HasColumnName("at_id");

            builder.HasOne(x => x.Idioma)
                .WithMany(i => i.IdiomaAtracciones)
                .HasForeignKey(x => x.IdId);

            builder.HasOne(x => x.Atraccion)
                .WithMany()
                .HasForeignKey(x => x.AtId);
        }
    }
}
