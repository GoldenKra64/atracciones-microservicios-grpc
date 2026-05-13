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
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("CATEGORIA");

            builder.HasKey(x => x.CatId);

            builder.Property(e => e.CatId).HasColumnName("cat_id");
            builder.Property(e => e.CatGuid).HasColumnName("cat_guid");

            builder.Property(e => e.CatParentId).HasColumnName("cat_parent_id");
            builder.Property(e => e.CatNombre).HasColumnName("cat_nombre");

            builder.Property(e => e.CatEstado).HasColumnName("cat_estado");

            builder.Property(x => x.CatNombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CatEstado)
                .IsRequired()
                .HasMaxLength(3);

            // 🔥 Self reference (padre-hijo)
            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.CatParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
