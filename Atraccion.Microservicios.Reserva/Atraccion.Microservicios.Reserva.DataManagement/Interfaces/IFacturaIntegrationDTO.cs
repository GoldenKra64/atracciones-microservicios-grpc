using Atraccion.Microservicios.Reserva.DataManagement.Integrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Interfaces
{
    public interface IFacturaIntegration
    {
        Task GenerateInvoiceAsync(GenerateInvoiceDto dto);
    }
}
