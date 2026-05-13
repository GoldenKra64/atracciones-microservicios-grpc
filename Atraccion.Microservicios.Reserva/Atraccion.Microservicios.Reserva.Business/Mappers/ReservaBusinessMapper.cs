using Atraccion.Microservicios.Reserva.Business.DTOs.Reserva;
using Atraccion.Microservicios.Reserva.DataManagement.Models.Reserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.Business.Mappers
{
    public static class ReservaBusinessMapper
    {
        // 🔹 Request → Model
        public static ReservaCreateModel ToCreateModel(CreateReservaRequest request)
        {
            return new ReservaCreateModel
            {
                ClienteId = request.ClienteId,

                Lineas = request.Lineas.Select(d => new DetalleReservaCreateModel
                {
                    TicketId = d.tck_guid,
                    Cantidad = d.cantidad
                }).ToList(),

                Canal = request.origen_canal,
                HorarioGuid = request.hor_guid
            };
        }

        // 🔹 Model → Response
        public static ReservaResponse ToResponse(ReservaModel model)
        {
            return new ReservaResponse
            {
                rev_guid = model.rev_guid,
                rev_codigo = model.rev_codigo,
                rev_estado = model.rev_estado,

                rev_subtotal = model.rev_subtotal,
                rev_valor_iva = model.rev_valor_iva,
                rev_total = model.rev_total,
                moneda = model.moneda,

                hor_fecha = model.hor_fecha,
                hor_hora_inicio = model.hor_hora_inicio,
                hor_hora_fin = model.hor_hora_fin,

                atraccion_nombre = model.atraccion_nombre,

                rev_fecha_reserva_utc = model.rev_fecha_reserva_utc,

                detalle = model.detalle?.Select(d => new DetalleReservaResponse
                {
                    tck_guid = d.tck_guid,
                    tck_tipo_participante = d.tck_tipo_participante,
                    cantidad = d.cantidad,
                    precio_unit = d.precio_unit,
                    subtotal = d.subtotal
                }).ToList(),
            };
        }

        public static UpdateReservaModel ToUpdateModel(UpdateReservaRequest model)
        {
            return new UpdateReservaModel
            {
                Id = model.Id,
                ClienteId = model.ClienteId,
                HorarioGuid = model.hor_guid,
                Canal = model.origen_canal,
                Lineas = model.Lineas.Select(x => new DetalleReservaCreateModel
                {
                    TicketId = x.tck_guid,
                    Cantidad = x.cantidad
                }).ToList()
            };
        }
    }
}
