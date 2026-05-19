using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.Business.DTOs.Reserva
{
    public class ReservaResponse : BaseResponse
    {
        public string rev_guid { get; set; } = string.Empty;
        public string rev_codigo { get; set; } = string.Empty;
        public string hor_fecha { get; set; } = string.Empty;
        public string hor_hora_inicio { get; set; } = string.Empty;
        public string? hor_hora_fin { get; set; } = string.Empty;
        public string atraccion_nombre { get; set; } = string.Empty;
        public double rev_subtotal { get; set; }
        public double rev_valor_iva { get; set; }
        public double rev_total { get; set; }
        public string? moneda { get; set; } = "USD";
        public string rev_estado { get; set; } = string.Empty;
        public string rev_fecha_reserva_utc { get; set; } = string.Empty;
        public List<DetalleReservaResponse> detalle { get; set; } = new();

        [System.Text.Json.Serialization.JsonPropertyName("_links")]
        public Dictionary<string, string> _links { get; set; } = new();
    }
}
