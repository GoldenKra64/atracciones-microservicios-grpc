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
    public class IdiomaConfiguration : IEntityTypeConfiguration<Idioma>
    {
        public void Configure(EntityTypeBuilder<Idioma> builder)
        {
            builder.ToTable("IDIOMA");

            builder.HasKey(x => x.IdId);

            builder.Property(e => e.IdId).HasColumnName("id_id");
            builder.Property(e => e.IdCodigo).HasColumnName("id_codigo");
            builder.Property(e => e.IdNombre).HasColumnName("id_descripcion");
            builder.Property(e => e.IdEstado).HasColumnName("id_estado");

            builder.Property(x => x.IdNombre)
                .IsRequired()
                .HasMaxLength(2);
        }
    }
}
