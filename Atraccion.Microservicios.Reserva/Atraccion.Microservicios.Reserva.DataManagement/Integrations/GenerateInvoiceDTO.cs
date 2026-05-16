using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Integrations
{
    public class GenerateInvoiceDto
    {
        public int RevId { get; set; }
        public int CliId { get; set; }
        public double Total { get; set; }
        public string Canal { get; set; }
        public string? NombreReceptor { get; set; }
        public string? CorreoReceptor { get; set; }
    }
}
