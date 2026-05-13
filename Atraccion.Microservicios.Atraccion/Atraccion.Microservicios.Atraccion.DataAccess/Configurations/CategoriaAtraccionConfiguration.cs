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
    public class CategoriaAtraccionConfiguration : IEntityTypeConfiguration<CategoriaAtraccion>
    {
        public void Configure(EntityTypeBuilder<CategoriaAtraccion> builder)
        {
            builder.ToTable("CATEGORIA_ATRACCION");

            builder.HasKey(x => new { x.CatId, x.AtId });

            builder.Property(e => e.CatId).HasColumnName("cat_id");
            builder.Property(e => e.AtId).HasColumnName("at_id");

            builder.HasOne(x => x.Categoria)
                .WithMany(c => c.CategoriaAtracciones)
                .HasForeignKey(x => x.CatId);

            builder.HasOne(x => x.Atraccion)
                .WithMany(a => a.CategoriaAtracciones)
                .HasForeignKey(x => x.AtId);
        }
    }
}
