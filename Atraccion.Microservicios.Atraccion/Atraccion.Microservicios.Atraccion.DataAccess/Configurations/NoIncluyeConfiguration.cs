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
    public class NoIncluyeConfiguration : IEntityTypeConfiguration<NoIncluye>
    {
        public void Configure(EntityTypeBuilder<NoIncluye> builder)
        {
            builder.ToTable("NOINCLUYE");

            builder.HasKey(x => x.NoIncId);

            builder.Property(e => e.NoIncId)
           .HasColumnName("noinc_id");

            builder.Property(e => e.NoIncDescripcion)
                   .HasColumnName("inc_descripcion")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(e => e.NoIncEstado)
                   .HasColumnName("inc_estado")
                   .HasMaxLength(3)
                   .IsRequired();

            builder.Property(x => x.NoIncDescripcion)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(e => e.NoIncluyeAtracciones)
               .WithOne(e => e.NoIncluye)
               .HasForeignKey(e => e.NoIncId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
