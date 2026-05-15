using Atraccion.Microservicios.Factura.DataManagement.Models.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Factura.DataManagement.Mappers
{
    public static class FacturaMapper
    {
        public static FacturaModel ToModel(Factura.DataAccess.Entities.Factura entity)
        {
            return new FacturaModel
            {
                Id = entity.FacId,
                Guid = entity.FacGuid,
                Estado = entity.FacEstado,
                Total = (double)entity.FacTotal,
                Numero = entity.FacNumero,
                FechaEmision = entity.FacFechaEmision.ToShortDateString(),
                OrigenCanal = entity.FacOrigenCanal,
                Observacion = entity.FacObservacion,
                RevCodigo = entity.RevId,
                NombreReceptor = entity.FacNombreReceptor,
                CorreoReceptor = entity.FacCorreoReceptor
            };
        }
    }
}
