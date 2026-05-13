using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataAccess.Entities
{
    public class DetalleReserva
    {
        public string DetRevGuid { get; set; }

        public int RevId { get; set; }
        public int TicId { get; set; }

        public string? TicTipoParticipante { get; set; }
        public string TicTitulo { get; set; } = null!;
        public int TicCantidad { get; set; }
        public double TicPrecioUnitario { get; set; }
        public double TicSubtotal { get; set; }

        // Navegación
        public Reserva Reserva { get; set; } = null!;
    }
}
