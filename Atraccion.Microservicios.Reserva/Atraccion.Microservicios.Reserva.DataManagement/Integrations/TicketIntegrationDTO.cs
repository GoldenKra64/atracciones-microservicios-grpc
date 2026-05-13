using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Integrations
{
    public class TicketIntegrationDto
    {
        public int TicId { get; set; }
        public string TicTitulo { get; set; }
        public double TicPrecio { get; set; }
        public string TicTipoParticipante { get; set; }
        public int HorId { get; set; }
    }
}
