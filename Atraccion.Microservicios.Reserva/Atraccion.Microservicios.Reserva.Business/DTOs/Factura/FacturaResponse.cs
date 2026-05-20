using System;
using System.Text.Json.Serialization;

namespace Atraccion.Microservicios.Reserva.Business.DTOs.Factura
{
    public class FacturaResponse
    {
        [JsonPropertyName("fac_guid")]
        public string FacGuid { get; set; } = string.Empty;

        [JsonPropertyName("fac_numero")]
        public string FacNumero { get; set; } = string.Empty;

        [JsonPropertyName("rev_codigo")]
        public string RevCodigo { get; set; } = string.Empty;

        [JsonPropertyName("total")]
        public double Total { get; set; }

        [JsonPropertyName("moneda")]
        public string Moneda { get; set; } = "USD";

        [JsonPropertyName("fecha_emision")]
        public string FechaEmision { get; set; } = string.Empty;

        [JsonPropertyName("estado")]
        public string Estado { get; set; } = string.Empty;

        [JsonPropertyName("nombre_receptor")]
        public string NombreReceptor { get; set; } = string.Empty;

        [JsonPropertyName("correo_receptor")]
        public string CorreoReceptor { get; set; } = string.Empty;
    }
}
