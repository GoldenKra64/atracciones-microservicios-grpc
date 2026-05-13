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
    public class IncluyeConfiguration : IEntityTypeConfiguration<Incluye>
    {
        public void Configure(EntityTypeBuilder<Incluye> builder)
        {
            builder.ToTable("INCLUYE");

            builder.HasKey(x => x.IncId);

            builder.Property(e => e.IncId)
           .HasColumnName("inc_id");

            builder.Property(e => e.IncDescripcion)
                   .HasColumnName("inc_descripcion")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(e => e.IncEstado)
                   .HasColumnName("inc_estado")
                   .HasMaxLength(3)
                   .IsRequired();

            builder.Property(x => x.IncDescripcion)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(e => e.IncluyeAtracciones)
               .WithOne(e => e.Incluye)
               .HasForeignKey(e => e.IncId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
