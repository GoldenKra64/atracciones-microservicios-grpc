using Atraccion.Microservicios.Factura.Business.DTOs.Factura;
using Atraccion.Microservicios.Factura.DataManagement.Models.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.Business.Mappers
{
    public static class FacturaBusinessMapper
    {
        public static FacturaResponse ToResponse(FacturaModel model)
        {
            return new FacturaResponse
            {
                Id = model.Id,
                fac_guid = model.Guid,
                estado = model.Estado,
                total = model.Total,
                Observacion = model.Observacion,
                fac_numero = model.Numero,
                fecha_emision = model.FechaEmision,
                OrigenCanal = model.OrigenCanal,
                rev_codigo = model.RevCodigo,
                nombre_receptor = model.NombreReceptor,
                correo_receptor = model.CorreoReceptor
            };
        }
    }
}
