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
            var links = new Dictionary<string, string>
            {
                { "self", $"/api/v2/reservas/{model.rev_guid}" }
            };

            if (model.rev_estado == "PEN")
            {
                links.Add("confirmar_pago", $"/api/v2/reservas/{model.rev_guid}/pagos/confirmacion");
            }

            var estadoMapeado = model.rev_estado switch
            {
                "PEN" => "PENDIENTE",
                "APR" => "APROBADO",
                "CAN" => "CANCELADO",
                _ => model.rev_estado
            };

            var fechaReservaIso = DateTime.TryParse(model.rev_fecha_reserva_utc, out var dt) 
                ? dt.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ") 
                : model.rev_fecha_reserva_utc;

            return new ReservaResponse
            {
                rev_guid = model.rev_guid,
                rev_codigo = model.rev_codigo,
                rev_estado = estadoMapeado,

                rev_subtotal = Math.Round(model.rev_subtotal, 2),
                rev_valor_iva = Math.Round(model.rev_valor_iva, 2),
                rev_total = Math.Round(model.rev_total, 2),
                moneda = model.moneda,

                hor_fecha = model.hor_fecha,
                hor_hora_inicio = model.hor_hora_inicio,
                hor_hora_fin = model.hor_hora_fin,
                hor_guid = model.hor_guid,

                atraccion_nombre = model.atraccion_nombre,

                rev_fecha_reserva_utc = fechaReservaIso,

                detalle = model.detalle?.Select(d => new DetalleReservaResponse
                {
                    tck_guid = d.tck_guid,
                    tck_tipo_participante = d.tck_tipo_participante,
                    cantidad = d.cantidad,
                    precio_unit = Math.Round(d.precio_unit, 2),
                    subtotal = Math.Round(d.subtotal, 2)
                }).ToList(),
                
                _links = links
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
