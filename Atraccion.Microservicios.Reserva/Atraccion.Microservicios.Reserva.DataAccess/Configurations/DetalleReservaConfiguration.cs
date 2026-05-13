using Atraccion.Microservicios.Reserva.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataAccess.Configurations
{
    public class DetalleReservaConfiguration : IEntityTypeConfiguration<DetalleReserva>
    {
        public void Configure(EntityTypeBuilder<DetalleReserva> builder)
        {
            builder.ToTable("RESERVA_DETALLE");

            // 🔑 PK
            builder.HasKey(e => new { e.RevId, e.TicId });

            // 🔗 Columnas
            builder.Property(e => e.DetRevGuid).HasColumnName("rdet_guid");

            builder.Property(e => e.RevId).HasColumnName("rev_id");
            builder.Property(e => e.TicId).HasColumnName("tck_id");

            builder.Property(e => e.TicTipoParticipante).HasColumnName("rdet_tipo_participante");
            builder.Property(e => e.TicPrecioUnitario).HasColumnName("rdet_precio_unit");
            builder.Property(e => e.TicSubtotal).HasColumnName("rdet_subtotal");

            builder.HasOne(x => x.Reserva)
                .WithMany(r => r.Detalles)
                .HasForeignKey(x => x.RevId);

        }
    }
}
